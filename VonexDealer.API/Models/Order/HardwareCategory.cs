using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    /// <summary>
    /// List of categories for breakup of list
    /// </summary>
    public class HardwareCategory
    {
        public int HardwareCategoryID { get; set; }
        public string Category { get; set; }

    }
}
