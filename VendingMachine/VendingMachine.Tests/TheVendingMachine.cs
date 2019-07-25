using System;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace VendingMachine.Tests
{
    [TestFixture]
    public class TheVendingMachine
    {
        private VendingMachine machine;

        [SetUp]
        public void SetUp()
        {
            var reportManager = new ReportManager();
            var cashManager = new CashManager(reportManager);
            var productManager = new ProductManager(reportManager);

            machine = new VendingMachine(cashManager, productManager);
        }

        [Test]
        public void Can_accept_money()
        {
            machine.Insert(Coin.TwoPound, Coin.OnePound);
        }

        [Test]
        public void Returns_coins_when_reject_is_pressed()
        {
            machine.SetChange(Coin.FiftyPence, Coin.TwentyPence);

            var insertedCoins = new[]
            {
                Coin.FiftyPence,
                Coin.TwentyPence
            };

            machine.Insert(insertedCoins);

            var returnedCoins = machine.Reject();
            returnedCoins.Should().BeEquivalentTo(insertedCoins);
        }

        [Test]
        public void Dispenses_product_and_no_change_when_product_is_selected()
        {
            const string location = "D3";

            machine.SetProduct(location, new Product { Price = 0.10M });

            var insertedCoins = new[]
            {
                Coin.TenPence
            };

            machine.Insert(insertedCoins);

            var product = machine.Select(location);
            product.Should().NotBeNull();

            var change = machine.Reject();
            change.Should().BeEmpty();
        }

        [Test]
        public void Dispenses_product_and_correct_change()
        {
            const string location = "A3";
            machine.SetProduct(location, new Product { Price = 1.5M });
            machine.SetChange(Coin.FiftyPence);

            var insertedCoins = new[]
            {
                Coin.TwoPound
            };

            machine.Insert(insertedCoins);

            var product = machine.Select(location);
            product.Price.Should().Be(1.5M);

            var change = machine.Reject();
            change.Should().BeEquivalentTo(new[] { Coin.FiftyPence });
        }

        [Test]
        public void Dispenses_product_and_correct_change_from_a_fiver()
        {
            const string location = "C5";
            machine.SetProduct(location, new Product { Price = 1.73M });
            machine.SetChange(Enum.GetValues(typeof(Coin)).Cast<Coin>().ToArray());

            var insertedCoins = new[]
            {
                Coin.TwoPound,
                Coin.TwoPound,
                Coin.OnePound
            };

            machine.Insert(insertedCoins);

            var product = machine.Select(location);
            product.Price.Should().Be(1.73M);

            var change = machine.Reject();
            change.Should().BeEquivalentTo(new[] { Coin.TwoPound, Coin.OnePound, Coin.TwentyPence, Coin.FivePence, Coin.TwoPence });
        }

        [Test]
        public void Cant_dispense_absent_product_but_can_dispense_alternate_product()
        {
            const string emptyLocation = "B7";
            const string stockedLocation = "B6";

            machine.SetProduct(stockedLocation, new Product { Price = 1.75M });

            var insertedCoins = new[]
            {
                Coin.OnePound,
                Coin.FiftyPence,
                Coin.TwentyPence,
                Coin.FivePence
            };

            machine.Insert(insertedCoins);

            var product = machine.Select(emptyLocation);
            product.Should().BeNull();

            var message = machine.GetMessage();
            message.Should().Be("No product available at B7");

            var alternateProduct = machine.Select(stockedLocation);
            alternateProduct.Price.Should().Be(1.75M);
        }

        [Test]
        public void Dispenses_two_products_and_correct_change_from_limited_cash_float()
        {
            const string stockedLocation1 = "C1";
            const string stockedLocation2 = "B4";

            machine.SetProduct(stockedLocation1, new Product { Price = 1.72M });
            machine.SetProduct(stockedLocation2, new Product { Price = 0.59M });

            var changeForFloat = new[]
            {
                Coin.FivePence, Coin.FivePence, Coin.FivePence, Coin.FivePence, Coin.FivePence,
                Coin.FivePence, Coin.FivePence, Coin.FivePence, Coin.FivePence, Coin.FivePence,
                Coin.TwentyPence, Coin.TwentyPence,
                Coin.OnePound, Coin.OnePound,
                Coin.OnePence, Coin.OnePence, Coin.OnePence, Coin.OnePence, Coin.OnePence,
                Coin.OnePence, Coin.OnePence,
                Coin.TwoPence
            };

            machine.SetChange(changeForFloat);

            var insertedCoins = new[]
            {
                Coin.TwoPound,
                Coin.TwoPound
            };

            machine.Insert(insertedCoins);

            var product = machine.Select(stockedLocation1);
            product.Price.Should().Be(1.72M);

            var product2 = machine.Select(stockedLocation2);
            product2.Price.Should().Be(0.59M);

            var change = machine.Reject();
            change.Should()
                .BeEquivalentTo(new[]
                {
                    Coin.OnePound, 
                    Coin.TwentyPence, Coin.TwentyPence, 
                    Coin.FivePence, Coin.FivePence, Coin.FivePence, Coin.FivePence, Coin.FivePence, 
                    Coin.TwoPence, 
                    Coin.OnePence, Coin.OnePence
                });
        }

        [Test]
        public void Dispenses_last_product_displays_message_and_returns_change_when_second_product_is_requested()
        {
            const string location = "F4";
            const int quantity = 1;
            machine.SetProduct(location, new Product { Price = 1M }, quantity);

            machine.SetChange(Enum.GetValues(typeof(Coin)).Cast<Coin>().ToArray());

            var insertedCoins = new[]
            {
                Coin.TwoPound
            };

            machine.Insert(insertedCoins);

            // Select first time
            var product = machine.Select(location);
            product.Price.Should().Be(1M);

            // Select second time
            var product2 = machine.Select(location);
            product2.Should().BeNull();

            var message = machine.GetMessage();
            message.Should().Be("No product available at F4");

            var change = machine.Reject();
            change.Should().BeEquivalentTo(new[] { Coin.OnePound });
        }

        [Test]
        public void Provides_Show_Stock_Used_report()
        {
            const string location1 = "A4";
            const string location2 = "A5";

            machine.SetProduct(location1, new Product { Price = 1.50M });
            machine.SetProduct(location2, new Product { Price = 1M }, 2);

            machine.SetChange(Enum.GetValues(typeof(Coin)).Cast<Coin>().ToArray());

            var insertedCoins = new[]
            {
                Coin.TwoPound
            };

            machine.Insert(insertedCoins);

            var firstProduct = machine.Select(location1);
            firstProduct.Price.Should().Be(1.50M);

            var insertedCoins2 = new[]
            {
                Coin.TwoPound
            };

            machine.Insert(insertedCoins2);

            var secondProduct = machine.Select(location2);
            secondProduct.Price.Should().Be(1M);
            var secondProduct2 = machine.Select(location2);
            secondProduct2.Price.Should().Be(1M);

            var stockReport = machine.GetStockReport();
            stockReport.QuantityDispensedFrom(location1).Should().Be(1);
            stockReport.QuantityDispensedFrom(location2).Should().Be(2);
        }

        [Test]
        public void Provides_Show_Money_Taken_report()
        {
            const string location1 = "E4";
            const string location2 = "E5";

            machine.SetProduct(location1, new Product { Price = 1.70M });
            machine.SetProduct(location2, new Product { Price = 1.2M }, 2);

            machine.SetChange(new[]
            {
                Coin.FiftyPence,
                Coin.TwentyPence,
                Coin.TenPence,
                Coin.TenPence,
                Coin.FivePence,
                Coin.FivePence,
                Coin.FivePence,
                Coin.FivePence,
                Coin.FivePence
            });

            var insertedCoins = new[]
            {
                Coin.TwoPound
            };

            machine.Insert(insertedCoins);

            var firstProduct = machine.Select(location1);
            firstProduct.Price.Should().Be(1.70M);

            var firstChange = machine.Reject();
            firstChange.ShouldBeEquivalentTo(new[] { Coin.TwentyPence, Coin.TenPence });

            var insertedCoins2 = new[]
            {
                Coin.TwoPound,
                Coin.OnePound
            };

            machine.Insert(insertedCoins2);

            var secondProduct = machine.Select(location2);
            secondProduct.Price.Should().Be(1.2M);
            var secondProduct2 = machine.Select(location2);
            secondProduct2.Price.Should().Be(1.2M);

            var secondChange = machine.Reject();
            secondChange.ShouldBeEquivalentTo(new[] { Coin.FiftyPence, Coin.TenPence });

            var moneyReport = machine.GetMoneyReport();
            moneyReport.NumberOf(Coin.TwoPound).Should().Be(2);
            moneyReport.NumberOf(Coin.OnePound).Should().Be(1);
            moneyReport.NumberOf(Coin.FiftyPence).Should().Be(0);
            moneyReport.NumberOf(Coin.TwentyPence).Should().Be(0);
            moneyReport.NumberOf(Coin.TenPence).Should().Be(0);
            moneyReport.NumberOf(Coin.FivePence).Should().Be(5);
        }

        [Test]
        public void Dials_back_to_head_office_when_stock_of_a_product_is_reduced_to_2()
        {
            var headOfficeConnection = Substitute.For<IExternalConnection>();
            machine.SetHeadOfficeConnection(headOfficeConnection);

            const string location1 = "E4";
            const int productId = 4;

            machine.SetProduct(location1, new Product { ProductId = productId, Price = 0.50M }, 3);

            machine.SetChange(Enum.GetValues(typeof(Coin)).Cast<Coin>().ToArray());

            var insertedCoins = new[]
            {
                Coin.TwoPound
            };

            machine.Insert(insertedCoins);

            var firstProduct = machine.Select(location1);
            firstProduct.Price.Should().Be(0.50M);

            var secondProduct = machine.Select(location1);
            secondProduct.Price.Should().Be(0.50M);

            headOfficeConnection.Received().StockNotificationFor(productId);

            var thirdProduct = machine.Select(location1);
            thirdProduct.Price.Should().Be(0.50M);

            var change = machine.Reject();
            change.ShouldBeEquivalentTo(new[] { Coin.FiftyPence });
        }
    }
}
