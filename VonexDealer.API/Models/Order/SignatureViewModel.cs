using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VonexDealer.API.Models
{
    public class SignatureViewModel
    {
        public Guid orderGUID { get; set; }
        public string data { get; set; }
    }
}
