using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace IChequeVendingMachine.Tests
{
    class VendingMachine
    {
        private Money _money;
        private Product _selectedProduct;
        private Change _availableChange;

        public void Insert(Money moneyValue)
        {
            _money = moneyValue;
        }

        public Money Reject()
        {
            
            return _money;
        }

        internal Product SelectProduct(string productCode)
        {
            _selectedProduct = new Product();

            var available = true;
            while (available)
            {
                switch (productCode)
                {
                    case "A2":
                        _selectedProduct.Price = 1.5M;
                        break;
                    case "A3":
                        _selectedProduct.Price = 1.72M;
                        break;
                    case "A4":
                        _selectedProduct.Price = 1.73M;
                        break;
                    case "B5":
                        _selectedProduct.Price = 0.59M;
                        break;
                    case "B6":
                        _selectedProduct.Price = 1.75M;
                        break;
                    case "B7":
                        _selectedProduct.Price = 1.75M;
                        _selectedProduct.Stock = 0;
                        available = false;
                        _selectedProduct.ErrorMsg = "No product avaiable at" + '-' + productCode;
                        break;
                }
            }
            
            return _selectedProduct;
        }

        public int StockAmount()
        {
            return 1;
        }

        public decimal ChangeAmount()
        {
            var availableChange = new Change();
            
            return Convert.ToDecimal((int)_money) - (100*_selectedProduct.Price);
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
        twoPound = 200
        //fivePound = 500
    }
}
