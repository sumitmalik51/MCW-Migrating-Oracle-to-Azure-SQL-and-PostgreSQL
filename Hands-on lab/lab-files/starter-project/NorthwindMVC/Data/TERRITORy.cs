using System;
using System.Collections.Generic;

#nullable disable

namespace NorthwindMVC.Data
{
    public partial class Territory
    {
        public string Territoryid { get; set; }
        public string Territorydescription { get; set; }
        public decimal Regionid { get; set; }

        public virtual Region Region { get; set; }
    }
}
