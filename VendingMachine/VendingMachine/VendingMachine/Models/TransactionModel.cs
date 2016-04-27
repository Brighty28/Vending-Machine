using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class TransactionModel
    {
        [Key]
        public int transactionId { get; set; }

        public bool success { get; set; }

        public DateTime transactionDate { get; set; }

        //public ProductModel transactionDetails { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
        //public string message { get; set; }
    }
}