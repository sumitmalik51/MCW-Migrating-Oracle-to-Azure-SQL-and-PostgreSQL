using System;
using System.Collections.Generic;

#nullable disable

namespace NorthwindMVC.Data
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
