using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        //public TransactionModel Transaction { get; set; }
    }

    public enum Money
    {
        [Display(Name = "£2")]
        twoPound = 200,
        [Display(Name = "£5")]
        fivepound =500,
        [Display(Name = "£1.75")]
        oneSeventyFive = 175,
        [Display(Name = "£1.72")]
        oneSeventyTwo = 172,
        [Display(Name = "£0.59")]
        fiftyNine = 059 
    }
}