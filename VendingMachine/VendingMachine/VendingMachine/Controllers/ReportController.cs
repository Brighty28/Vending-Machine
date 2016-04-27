using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    public class ReportController : Controller
    {
        VendingEntities db = new VendingEntities();

        // GET: Report
        public ActionResult Stock_Report()
        {
            var transactions = db.Transactions.ToList();

            return View(transactions);
        }
    }
}