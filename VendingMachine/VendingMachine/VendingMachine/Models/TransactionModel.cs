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

        public bool status { get; set; }

        public DateTime transactionDate { get; set; }

        public string message { get; set; }
    }
}