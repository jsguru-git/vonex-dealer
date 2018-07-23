using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Models.Order
{
    /// <summary>
    /// Order will be comprised of multiple sections as per the original pdf document
    /// customer details - which incorprate Company
    /// IP order details
    /// Hardware order details
    /// PSTN(landline) Details
    /// Internet Details
    /// Porting Authority
    /// </summary>
    public class Order
    {
        public Int64 OrderID { get; set; }
        public Guid OrderGUID { get; set; }
        public int OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public Int64 DealerID { get; set; }
        public Dealer DealerDetails { get; set; }

        public Int64 CustomerID { get; set; }
        public Customer CustomerDetails { get; set; }

       
        public Int64? DirectDebitID { get; set; }
        //public DirectDebit DirectDebitDetails { get; set; }

        public Int64 HardwareID { get; set; }
        /// <summary>
        /// delivery address infomation
        /// </summary>
        //public Hardware HardwareDetails { get; set; }

        public Int64? IPOrderID { get; set; }
        //public IPOrder IPOrderDetails { get; set; }

        public Int64? LandlineID { get; set; }
        //public Landline LandlineDetails { get; set; }

        public Int64? InternetID { get; set; }
        //public Internet InternetDetails { get; set; }

        public string Signature { get; set; }

    }

        enum Status
        {
            New = 0,
            InProgress = 1,
            Signed = 2,
            Completed = 3,
            Cancelled = 4,
            Rejected = 5
        }
}
