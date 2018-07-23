using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class HardwareCount
    {
        public int ID { get; set; }
        public Int64 OrderID { get; set; }
        public Int64 HandsetID { get; set; }
        public double Cost { get; set; }
        public bool DealerSupplied { get; set; }

    }

    public class HardwareCountSummary
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public Int64 HandsetID { get; set; }
        public double Cost { get; set; }
        public bool DealerSupplied { get; set; }
    }



}
