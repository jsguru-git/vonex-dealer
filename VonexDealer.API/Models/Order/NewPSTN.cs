using System;

namespace VonexDealer.API.Models.Order
{
    public class NewPSTN
    {
        public Int64  NewPSTNID { get; set; }
        public Int64 AddressID { get; set; }
        public String Termination { get; set; }
        public double Fee { get; set; }
        public Int64 RatePlanID { get; set; }
        public Int64 LandlineID { get; set; }
        public int ContractPeriod { get; set; }
    }


}