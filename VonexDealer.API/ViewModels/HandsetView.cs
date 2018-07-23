using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.ViewModels
{
    public class HandsetView
    {
        public Handset Handset   { get; set; }
        public HardwareCategory HardwareCategory { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
