using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    /// <summary>
    /// QA list of Manufacturers.
    /// </summary>
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        public string Description { get; set; }
    }
}
