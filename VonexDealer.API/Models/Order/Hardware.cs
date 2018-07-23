using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class Hardware
    {
        public Int64 HardwareID { get; set; }
        public Int64 HardwareOrderID { get; set; }
        public int Quantity { get; set; }
        public Int64 HandsetID { get; set; }
        public string Comment { get; set; }
        public bool DealerSupplied { get; set; }
        public double Cost { get; set; }
        public double LineTotal { get; set; }
        public bool FromIPOrder { get; set; }

    }
}
