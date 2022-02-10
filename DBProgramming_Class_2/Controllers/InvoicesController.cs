using DBProgramming_Class_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgramming_Class_2.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        public ActionResult Index(string id)
        {
            var context = new BooksEntities();
            List<int> invoiceLineItems = context.InvoiceLineItems
                .Where(x => x.ProductCode == id)
                .Select(x => x.InvoiceID)
                .ToList();

            //get all invoices related to the productCode
            List<Invoice> invoices = context.Invoices
                .Where(x => invoiceLineItems
                    .Contains(x.InvoiceID))
                .OrderBy(x => x.InvoiceID)
                .ToList();

            return View(invoices);
        }

        public ActionResult InvoiceList()
        {
            var context = new BooksEntities();

            List<Invoice> invoices = context.Invoices
                .OrderBy(x => x.InvoiceID)
                .ToList();

            return View(invoices);
        }

        public ActionResult CustomerInvoices(int id)
        {
            var context = new BooksEntities();

            List<Invoice> invoices = new List<Invoice>();

            if (id != null && id != 0)
            {
                invoices = context.Invoices
                .Where(x => x.CustomerID == id)
                .ToList();
            }

            return View(invoices);
        }

        public ActionResult AddInvoice(int id = 0, int customerId = 0)
        {
            var context = new BooksEntities();

            Invoice invoice = context.Invoices.FirstOrDefault(x => x.InvoiceID == id);
            List<Customer> customers = context.Customers.ToList();

            if(id == 0 || invoice == null)
            {
                invoice = new Invoice();
            }

            if(customerId != 0)
            {
                customers = context.Customers.Where(c => c.CustomerID == customerId).ToList();
            }

            CombinedInvoiceCustomer cInvCus = new CombinedInvoiceCustomer();
            cInvCus.Invoice = invoice;
            cInvCus.Customers = customers;

            return View(cInvCus);
        }

        [HttpPost]
        public ActionResult AddInvoice(Invoice invoice)
        {
            var context = new BooksEntities();

            try
            {
                context.Invoices.AddOrUpdate(invoice);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //log error in database or in text file
            }

            return Redirect("/Invoices/AddInvoice/" + 0);
        }

        public ActionResult Delete(int id)
        {
            var context = new BooksEntities();

            Invoice invoice = context.Invoices.FirstOrDefault(x => x.InvoiceID == id);

            context.Invoices.Remove(invoice);
            context.SaveChanges();

            return Redirect("/Invoices/InvoiceList");
        }
    }
}