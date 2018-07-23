using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Repository;

namespace VonexDealer.API.Models.Order
{
    public class Landline
    {
        public Int64 LandlineID { get; set; }
        public Int64 OrderID { get; set; }
        //public List<Address> Addresses { get; set; }

        public List<Churn> Churns { get; set; }
        public List<NewPSTN> NewServices { get; set; }
        public List<ISDN> ISDNs { get; set; }

        public static implicit operator Landline(LandlineRepository v)
        {
            throw new NotImplementedException();
        }
    }
}
