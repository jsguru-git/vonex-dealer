using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class Auth
    {
        public Int64 AuthID { get; set; }
        public string Email { get; set; }
        public int AuthLevel { get; set; }
    }
}
