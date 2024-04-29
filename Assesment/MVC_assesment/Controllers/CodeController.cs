using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_assesment.Controllers
{
    public class CodeController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities(); // Update NorthwindEntities with your DbContext class name

        public ActionResult CustomersInGermany()
        {
            var customers = db.Customers.Where(c => c.Country == "Germany").ToList();
            return View(customers);
        }

        public ActionResult CustomerDetailsByOrderId()
        {
            var customer = db.Customers.FirstOrDefault(c => c.Orders.Any(o => o.OrderID == 10248));
            return View(customer);
        }
    }

}