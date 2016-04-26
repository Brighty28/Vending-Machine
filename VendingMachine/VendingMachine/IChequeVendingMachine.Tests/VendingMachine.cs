using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

            //var available = true;
            //while (available)
            //{
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
                    case "A5":
                        _selectedProduct.Price = 0.59M;
                        break;
                    case "B7":
                        _selectedProduct.Price = 1.75M;
                        _selectedProduct.Stock = 0;
                        
                        _selectedProduct.ErrorMsg = "No product avaiable at" + '-' + productCode;
                        break;
                    case "B6":
                        _selectedProduct.Price = 1.75M;
                        break;
                }
            //}
            
            return _selectedProduct;
        }

        internal Change ReplenishChange()
        {
            _availableChange = new Change();

            return _availableChange;
        }

        public void DeductChange(decimal change)
        {
            ReplenishChange();

            decimal runningTotal = 0;
            decimal existingChange = change;
            while(runningTotal != change)
            {
                if (existingChange >= (decimal)Money.onePound)
                {
                    _availableChange.UpdateChangeCount((int) Money.onePound, 1);
                    runningTotal += (decimal)Money.onePound;
                    existingChange = existingChange - (decimal)Money.onePound;  
                }
                else if (existingChange >= (decimal)Money.twentyPence && _availableChange.twentyPenceCount > 0)
                {
                    _availableChange.UpdateChangeCount((int) Money.twentyPence, 2);
                    runningTotal += (decimal)Money.twentyPence * 2;
                    existingChange = existingChange - (decimal)Money.twentyPence * 2;
                }
                else if (existingChange >= (decimal)Money.fivePence)
                {
                    _availableChange.UpdateChangeCount((int) Money.fivePence, 5);
                    runningTotal += (decimal)Money.fivePence * 5;
                    existingChange = existingChange - (decimal)Money.fivePence * 5;  
                }
                else if (existingChange >= (decimal)Money.twoPence && _availableChange.twoPenceCount > 0)
                {
                    _availableChange.UpdateChangeCount((int)Money.twoPence, 1);
                    runningTotal += (decimal)Money.twoPence;
                    existingChange = existingChange - (decimal)Money.twoPence;  

                }
                else if (existingChange >= (decimal)Money.penny)
                {
                    _availableChange.UpdateChangeCount((int)Money.penny, 2);
                    runningTotal += (decimal)Money.penny * 2;
                    existingChange = existingChange - (decimal)Money.penny * 2;  

                }
            }

            //return runningTotal;
        }

        public decimal ChangeAmount()
        {
            return Convert.ToDecimal((int)_money) - (100*_selectedProduct.Price);
        }

        public decimal ChangeAmount(List<Product> products)
        {
            decimal total = 0M;
            var i = 0;
            foreach (var product in products)
            {
                if (i == 0)
                {
                    total = Convert.ToDecimal((int) _money) - (100*product.Price);
                }
                else
                {
                    total = total - (100*product.Price);
                }
                i++;
            }
            return total;
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
