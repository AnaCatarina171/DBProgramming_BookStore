using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBProgramming_Class_2.Models
{
    public class CombinedInvoiceCustomer
    {
        //public List<State> States { get; set; }

        //public List<State> Products { get; set; }

        public List<Customer> Customers { get; set; }

        public Invoice Invoice { get; set; }
    }
}