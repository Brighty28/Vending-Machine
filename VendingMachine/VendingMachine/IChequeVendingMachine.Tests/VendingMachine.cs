﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace IChequeVendingMachine.Tests
{
    class VendingMachine
    {
        private Money money;
        private Product selectedProduct;

        public void Insert(Money moneyValue)
        {
            money = moneyValue;
        }

        public Money Reject()
        {
            
            return money;
        }

        internal Product SelectProduct(string productCode)
        {
            selectedProduct = new Product();

            switch (productCode)
            {
                case "A2":
                    selectedProduct.Price = 1.5M;
                    break;
                case "A4":
                    selectedProduct.Price = 1.73M;
                    break;
            }
            
            return selectedProduct;
        }

        public decimal ChangeAmount()
        {
            return Convert.ToDecimal((int)money) - (100*selectedProduct.Price);
        }
    }

    public enum Money
    {
        penny = 1,
        twoPence = 2,
        fivePence = 5,
        tenPence = 10,
        twentyPence = 20,
        fiftyPence = 50,
        onePound = 100,
        twoPound = 200,
        fivePound = 500
    }
}
