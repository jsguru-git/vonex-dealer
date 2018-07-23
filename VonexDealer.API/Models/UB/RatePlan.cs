using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.UB
{
    public class RatePlan
    {
        public int ID { get; set; }

        public int? RatePlanID { get; set; }

        public string RatePlanDescription { get; set; }

        public string GroupName { get; set; }

        public string UsageType { get; set; }

        public bool? Handset { get; set; }

        public decimal? MonthlyRetail { get; set; }

        public Single? MonthlyProfitPercent { get; set; }

        public string RatePlanPDF { get; set; }

        public int? DataAllowance { get; set; }

        public bool? MonitorDataUsage { get; set; }

        public int? ContractMonths { get; set; }

        public bool? PayInitialComms { get; set; }

        public bool? HasSpecialOverride { get; set; }

        public bool DealerSupplied { get; set; }
        public bool MixnMatch { get; set; }
        public bool AdditionalServices { get; set; }
        public bool Residential { get; set; }

    }



}
