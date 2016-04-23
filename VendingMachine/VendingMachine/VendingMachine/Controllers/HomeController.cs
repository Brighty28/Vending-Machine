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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}