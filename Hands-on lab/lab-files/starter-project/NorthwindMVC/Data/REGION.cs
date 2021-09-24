using System;
using System.Collections.Generic;

#nullable disable

namespace NorthwindMVC.Data
{
    public partial class Region
    {
        public Region()
        {
            Territories = new HashSet<Territory>();
        }

        public decimal Regionid { get; set; }
        public string Regiondescription { get; set; }

        public virtual ICollection<Territory> Territories { get; set; }
    }
}
