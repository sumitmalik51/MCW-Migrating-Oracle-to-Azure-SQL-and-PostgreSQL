using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindMVC.Models
{
    public class SalesByYear
    {
        public DateTime ShippedDate { get; set; }

        public decimal OrderID { get; set; }

        public double Subtotal { get; set; }

        public string Year { get; set; }
    }
}
