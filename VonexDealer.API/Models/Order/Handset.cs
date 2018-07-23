using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    /// <summary>
    /// Handset details for purchase, dealer and customer.
    /// </summary>
    public class Handset
    {
        public Int64 HandsetID { get; set; }
       
        public string Model { get; set; }
        /// <summary>
        /// use this value to order screen to show model.
        /// </summary>
        public string Description { get; set; }
        public string ImagePath { get; set; }

        /// <summary>
        /// exclude Dealerprice from customerview
        /// </summary>
        public Int64 HardwareCategoryID { get; set; }
        [NotMapped]
        public HardwareCategory HardwareCategory { get; set; }
        public Int64 ManufacturerDetailsID { get; set; }
       [NotMapped]
        public Manufacturer Manufacturer { get; set; }

        public double DealerEx { get; set; }
        public double RRPEx { get; set; }


    }

   
    
}
