using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class Internet
    {
        public Int64 InternetID { get; set; }
        public Int64 OrderID { get; set; }
        public string InternetType { get; set; }
        public Int64 RatePlanID { get; set; }
        public string UsageAllowance { get; set; }
        public string SpeedRequested { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal ConnectionFee { get; set; }
        public string ContractTerm { get; set; }
        public Int64 AddressID { get; set; }
        public string ServiceNumber { get; set; }
        public Int64 ContactID { get; set; }

    }
}
