using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace VonexDealer.API.Models.Order
{
    public class ISDN
    {
        public Int64 ISDNID { get; set; }
        public Int64 LandlineID { get; set; }

        public int ContractLengthMonths { get; set; }
        public Int64 RatePlanID { get; set; }
        public bool HasRange { get; set; }
        public int RangeSize { get; set; }
        public Int64 AddressID { get; set; }
        public string NumbersAsString { get; set; }
        [NotMapped]
        public List<String> Numbers
        {
            get
            {
                if (NumbersAsString != null)
                    return NumbersAsString.Split(',').ToList();

                return null;

            }
            set
            {
                NumbersAsString = String.Join(",", value);
            }
        }

    }
}