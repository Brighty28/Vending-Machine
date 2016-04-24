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
        //private Money _money;
        //private ProductModel _selectedProduct;
        private VendingEntities db = new VendingEntities();

        public ActionResult Index()
        {
            var products = db.Products.ToList();

            return View(products);
        }

        public ActionResult Purchase(int id, Money money )
        {
            var transaction = new TransactionModel();
            var Message = string.Empty;
            var product = db.Products.Single(p => p.Id == id);

            if(product.Stock > 0)
            {
                if((decimal)money == product.Price)
                {
                    product.Stock = -1;

                    transaction.transactionDate = DateTime.Now;
                    transaction.status = true;
                    db.SaveChanges();
                }
                else
                {
                    var amount = ChangeAmount((Money)money, product.Price);

                    product.Stock = - 1;
                    db.SaveChanges();

                    Message = "Please take your product and change" + '-' + amount;
                }

                Message = "Please take your product" + '-' + product.ProductName;
            }
            
            return PartialView("_purchaseResult", Message = "Product" +'-'+ product.ProductName +'-'+ "is currently out of stock");

        }

        public decimal ChangeAmount(Money money, decimal selectedProductPrice)
        {
            return Convert.ToDecimal((int)money) - (100 * selectedProductPrice);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}