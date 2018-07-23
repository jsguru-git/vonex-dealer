using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VonexDealer.API.Models.Order
{
    public class Churn
    {
        public Int64 ChurnID { get; set; }
        public Int64 LandlineID { get; set; }
        public string ContractPeriod { get; set; }
        public string ServiceNumber { get; set; }
        public Int64 RatePlanID { get; set; }
        public Int64 AddressID { get; set; }
      [JsonIgnore]
      [NotMapped]
        public string FormattedAddress { get; set; }

    }
}