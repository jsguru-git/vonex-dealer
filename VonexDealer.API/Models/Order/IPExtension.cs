using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Models.Order
{
    public class IPExtension
    {
        public Int64 IPExtensionID { get; set; }
        public Int64? OrderID { get; set; }
        public string ExtensionName { get; set; }
        /// <summary>
        /// starting at 500
        /// </summary>
        public int ExtensionNo { get; set; }
        public Int64 HandsetID { get; set; }
        public Int64 RatePlanID { get; set; }
        public string OutboundANI { get; set; }
        public bool Voicemail { get; set; }
        public string VoicemailEmail { get; set; }
        public string MobileTwinning { get; set; }
        public int contractLengthMonths { get; set; }
        public bool DealerSupplied { get; set; }

    }

   
}
