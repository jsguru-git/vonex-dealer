using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class HardwareOrder
    {
        public Int64 HardwareOrderID { get; set; }
        public Int64 OrderID { get; set; }
        public Int64 AddressID { get; set; }
        public List<Hardware> HardwareOrderDetails { get; set; }

        public double SubTotal { get; set; }
        public double Freight { get; set; }
        public double TotalInitialPayment
        {
            get
            {
                return SubTotal + Freight;
            }
        }
    }
}
