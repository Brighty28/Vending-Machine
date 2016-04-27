using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    public class HomeController : Controller
    {
        private static Money _money;
        private VendingEntities db = new VendingEntities();

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(ProductModel productModel)
        {
            _money = productModel.MoneyValue;
            TempData["insertedMoney"] = "You have inserted £" + Convert.ToDecimal((int)_money);
            return View();
        }

        public ActionResult Purchase()
        {
            var products = db.Products.ToList();

            return View(products);
        }

        //[HttpPost]
        public ActionResult PurchaseDetails(int id)//, Money money)
        {
            var transaction = new TransactionModel();

            var product = db.Products.Single(p => p.Id == id);

            transaction.transactionDate = DateTime.Now;
            transaction.success = false;
            transaction.ProductName = product.ProductName;
            transaction.Price = product.Price;
            transaction.Stock = product.Stock;

            if(product.Stock > 0) 
            {
                transaction.success = true;

                if((decimal)_money == product.Price)
                {
                    product.Stock -- ;
                    db.SaveChanges();

                    TempData["success"] = "Please take your product" + '-' + product.ProductName;
                }
                else 
                {
                    var amount = ChangeAmount(_money, product.Price);

                    product.Stock -- ;
                    db.SaveChanges();

                    TempData["change"] = "Please take your product and change" + '-' + amount;
                }
            }
            else
            {
                TempData["failure"] = "Product" + '-' + product.ProductName + '-' + "is currently out of stock";
            }

            //Add Transaction object into Transactions DBset
            db.Transactions.Add(transaction);

            // call SaveChanges method to save student into database
            db.SaveChanges();

            return View();

        }

        public decimal ChangeAmount(Money money, decimal selectedProductPrice)
        {
            return Convert.ToDecimal((int)money) - (100 * selectedProductPrice);
        }

    }
}