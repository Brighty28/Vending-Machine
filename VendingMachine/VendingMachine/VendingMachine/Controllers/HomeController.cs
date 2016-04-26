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
        private VendingEntities db = new VendingEntities();

        public ActionResult Index()
        {
            var products = db.Products.ToList();

            return View(products);
        }

        //[HttpPost]
        public ActionResult Purchase(int id, Money money)
        {
            var transaction = new TransactionModel();

            var product = db.Products.Single(p => p.Id == id);

            transaction.transactionDate = DateTime.Now;
            transaction.success = false;
            transaction.transactionDetails = product;

            if(product.Stock > 0) 
            {
                transaction.success = true;

                if((decimal)money == product.Price)
                {
                    product.Stock -- ;
                    db.SaveChanges();

                    TempData["message"] = "Please take your product" + '-' + product.ProductName;
                }
                else 
                {
                    var amount = ChangeAmount((Money)money, product.Price);

                    product.Stock -- ;
                    db.SaveChanges();

                    TempData["message"] = "Please take your product and change" + '-' + amount;
                }
            }
            else
            {
                TempData["message"] = "Product" + '-' + product.ProductName + '-' + "is currently out of stock";
            }
            
            return RedirectToAction("Index");

        }

        public decimal ChangeAmount(Money money, decimal selectedProductPrice)
        {
            return Convert.ToDecimal((int)money) - (100 * selectedProductPrice);
        }

    }
}