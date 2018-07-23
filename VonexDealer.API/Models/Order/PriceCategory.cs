using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class PriceCategory
    {
        public Int64 PriceCategoryID { get; set; }
        public bool GstExclusive { get; set; }
        public string Description { get; set; }

    }
}
