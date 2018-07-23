using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    /// <summary>
    /// Existing Dealer information taken from Utilibill and OMS system.
    /// </summary>
    public class Dealer
    {
        public Int64 DealerID { get; set; }
        public Int64 UBDealerID { get; set; }
        [Required]
        public string DealerFullName { get; set; }
        [Required]
        public string DealerEmail { get; set; }

    }
}
