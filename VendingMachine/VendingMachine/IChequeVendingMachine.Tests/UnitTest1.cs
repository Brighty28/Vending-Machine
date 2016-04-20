﻿using System;
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
        public void Should_Pass_First_Test()
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

            var pounds = (int)Money.onePound * 5;
            var insertedMoney = (Money)pounds;

            vendingMachine.Insert(insertedMoney);
            //vendingMachine.Coins(poinds, 5);

            Product product = vendingMachine.SelectProduct("A4");
            product.Should().NotBeNull();
            product.Price.Should().Be(1.73M);
            Decimal amount = vendingMachine.ChangeAmount();

            amount.Should().Be(327);
        }

        [Test]
        public void Successful_First_Selection_Outofstock()
        {
            var vendingMachine = new VendingMachine();

            const Money pound = Money.onePound;

            const int pence = (int) Money.fivePence + (int) Money.twentyPence + (int)Money.fiftyPence;

            var insertedMoney = pound + pence;

            vendingMachine.Insert(insertedMoney);
            
            Product product1 = vendingMachine.SelectProduct("B7");
            product1.Should().NotBeNull();
            //int stock = product.Stock;
            product1.Stock.Should().Equals(0);
            //string error = product.ErrorMsg;
            //product1.ErrorMsg.Should().NotBeEmpty();

            Product product2 = vendingMachine.SelectProduct("B6");

            product2.Price.Should().Be(1.75M);
            Decimal amount = vendingMachine.ChangeAmount();

            amount.Should().Be(0);
        }
    }
}
