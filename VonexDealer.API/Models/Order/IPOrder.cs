using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Models.Order
{

    public class IPOrder
    {
        public Int64 IPOrderID { get; set; }
        public Int64 OrderID { get; set; }
        public Int64? RatePlanID { get; set; }
        public string CurrentModem { get; set; }
        public string CurrentSwitch { get; set; }
        public string UploadSpeed { get; set; }
        public string DownloadSpeed { get; set; }
        public bool IsCreatedByAMPT { get; set; }
        public string AMPTDomainName { get; set; }
        public CallHandling callHandling { get; set; }
        public bool DealerSupplied { get; set; }
        public bool MinMonthly { get; set; }
    }

    /// <summary>
    /// Call Handling options for form submission.
    /// </summary>
    public enum CallHandling
    {
        [Description("Direct To Extension")]
        DirectToExtension,
        [Description("Hunt Group")]
        HuntGroup ,
        [Description("AutoAttendant (IVR)")]
        AutoAttendant 

    }


}
