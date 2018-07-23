using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class HardwareOrderDetail
    {
        public Int64 HardwareOrderDetailID { get; set; }
        public Int64 HardwareOrderID { get; set; }
        public Handset handset { get; set; }

       
    }
}
