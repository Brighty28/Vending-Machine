using System;
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
                case "B7":
                    selectedProduct.Price = 1.75M;
                    selectedProduct.Stock = 0;
                    selectedProduct.ErrorMsg = "No product avaiable at" + '-' + productCode;
                    break;
                case "B6":
                    selectedProduct.Price = 1.75M;
                    break;
            }
            
            return selectedProduct;
        }

        public int StockAmount()
        {
            return 1;
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
