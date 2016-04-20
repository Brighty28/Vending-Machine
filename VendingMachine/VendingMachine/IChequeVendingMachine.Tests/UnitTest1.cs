using System;
using FluentAssertions;
using NUnit.Framework;

namespace IChequeVendingMachine.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        #region -- Global Properties --

        //const vendingMachine = new VendingMachine();

        #endregion

        [Test]
        public void Should_pass_first_test()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.Insert(Money.penny);
        }

        [Test]
        public void Cancel_Transaction()
        {
            var vendingMachine = new VendingMachine();

            var insertedMoney = Money.penny;

            vendingMachine.Insert(insertedMoney);

            Money rejectedMoney = vendingMachine.Reject();

            rejectedMoney.Should().Be(insertedMoney);
        }

        [Test]
        public void Successful_Transaction()
        {
            var vendingMachine = new VendingMachine();

            var insertedMoney = Money.penny;

            vendingMachine.Insert(insertedMoney);

            Product product = vendingMachine.SelectProduct("A2");
            product.Should().NotBeNull();
        }

        [Test]
        public void Successful_Transaction_With_Price()
        {
            var vendingMachine = new VendingMachine();

            var insertedMoney = Money.twoPound;

            vendingMachine.Insert(insertedMoney);

            Product product = vendingMachine.SelectProduct("A2");
            product.Should().NotBeNull();
            product.Price.Should().Be(1.50M);
            Decimal amount = vendingMachine.ChangeAmount();

            amount.Should().Be(50);
        }

        [Test]
        public void Successful_Transaction_Higher_Price()
        {
            var vendingMachine = new VendingMachine();

            var insertedMoney = Money.fivePound;

            vendingMachine.Insert(insertedMoney);

            Product product = vendingMachine.SelectProduct("A4");
            product.Should().NotBeNull();
            product.Price.Should().Be(1.73M);
            Decimal amount = vendingMachine.ChangeAmount();

            amount.Should().Be(325);
        }

        [Test]
        public void Successful_First_Selection_Outofstock()
        {
            var vendingMachine = new VendingMachine();

            var insertedMoney = Money.onePound;

            vendingMachine.Insert(insertedMoney);

            Product product = vendingMachine.SelectProduct("B7");
            product.Should().NotBeNull();
            //int stock = vendingMachine.StockAmount();
            product.Stock.Should().NotBe(0);
            product.ErrorMsg.Should().BeNullOrEmpty();
            product.Price.Should().Be(1.75M);
            Decimal amount = vendingMachine.ChangeAmount();

            amount.Should().Be(0);
        }
    }
}
