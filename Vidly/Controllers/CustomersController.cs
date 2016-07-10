using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("Customers")]
        public ActionResult Index()
        {
            List<Customer> customers = _context.Customers.Include(c => c.MembershipType).ToList();
            CustomerViewModel customerViewModel = new CustomerViewModel() { Customers = customers };
            return View(customerViewModel);
        }

        [HttpGet]
        [Route("Customers/{id}")]
        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var customerFormViewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", customerFormViewModel);
        }

        [HttpGet]
        [Route("Customers/New")]
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var newCustomerViewModel = new CustomerFormViewModel() { MembershipTypes = membershipTypes };
            return View("CustomerForm", newCustomerViewModel);
        }

        [HttpPost]
        [Route("Customers")]
        public ActionResult Save(CustomerFormViewModel viewModel)
        {
            if (viewModel.Customer.Id == 0)
            {
                _context.Customers.Add(viewModel.Customer);
            }
            else
            {
                var customerFromDB = _context.Customers.Single(c => c.Id == viewModel.Customer.Id);
                customerFromDB.Name = viewModel.Customer.Name;
                customerFromDB.Birthdate = viewModel.Customer.Birthdate;
                customerFromDB.MembershipTypeId = viewModel.Customer.MembershipTypeId;
                customerFromDB.IsSuscribedToNewsletter = viewModel.Customer.IsSuscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
    }
}