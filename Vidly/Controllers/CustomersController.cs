using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly List<Customer> customers = new List<Customer>()
        {
            new Customer() { Id = 1, Name = "John Smith" },
            new Customer() { Id = 2, Name = "Mary Williams" }
        };


        // GET: Customers
        public ActionResult Index()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel() { Customers = customers };
            return View(customerViewModel);
        }

        [Route("Customers/{id}")]
        public ActionResult Customers(int id)
        {
            foreach (var c in customers)
            {
                if (c.Id == id)
                {
                    return View(c);
                }
            }

            return HttpNotFound();
        }
    }
}