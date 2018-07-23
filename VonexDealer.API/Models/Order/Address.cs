using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    /// <summary>
    /// this data should be collected from TotalCheck verification process - TCAddress
    /// </summary>
    public class Address
    {
        public Int64 AddressID { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int PostCode { get; set; }
        public Int64? CustomerID { get; set; }
        [NotMapped]
        public string FormattedAddress
        {
            get
            {
                return $"{StreetAddress}, {Suburb}, {State}, {PostCode}";
            }
        }
    }
}
