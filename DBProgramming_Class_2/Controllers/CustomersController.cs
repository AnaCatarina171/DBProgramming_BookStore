using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBProgramming_Class_2.Models;

namespace DBProgramming_Class_2.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customer
        public ActionResult Index(int id)
        {
            var context = new BooksEntities();

            if (id == 0 || id == null)
            {
                return Redirect("/Customers/AddCustomer");
            }

            Customer customer = context.Customers.FirstOrDefault(x => x.CustomerID == id);

            return View(customer);
        }

        public ActionResult CustomersList(string searchTerm)
        {
            var context = new BooksEntities();

            List<Customer> customers = context.Customers.OrderBy(c => c.Name).ToList();

            /*string searchTermName = Request.QueryString.Get("searchTermName");*/

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                customers = customers
                        .Where(c => c.Name.ToLower().IndexOf(searchTerm) != -1)
                        .ToList();
            }

            return View(customers);
        }

        public ActionResult Delete(int id)
        {
            var context = new BooksEntities();

            List<Customer> customers = context.Customers.ToList();

            Customer customer = context.Customers.FirstOrDefault(x => x.CustomerID == id);

            try
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //
            }

            return Redirect("/Customers/CustomersList");
        }

        public ActionResult AddCustomer(int id = 0)
        {
            var context = new BooksEntities();

            Customer customer = context.Customers.FirstOrDefault(x => x.CustomerID == id);

            if(id == 0 || customer == null)
            {
                customer = new Customer();
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            var context = new BooksEntities();

            try
            {
                context.Customers.AddOrUpdate(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //log error in database or in text file
            }

            return Redirect("/Customers/AddCustomer/" + 0);
        }
    }
}