using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.ViewModels
{
    public class OrderReport
    {
        public Int64 orderID { get; set; }
        public string htmlContent { get; set; }
        public Order order { get; set; }

    }
}
