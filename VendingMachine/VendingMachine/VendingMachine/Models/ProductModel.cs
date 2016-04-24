using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public Money MoneyValue { get; set; }

        public TransactionModel transaction { get; set; }
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
    }
}