using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class Contact
    {
        public Int64 ContactID { get; set; }
        public Int64 OrderID { get; set; }
        public string ContactName { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }

    }
}
