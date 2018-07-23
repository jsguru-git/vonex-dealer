using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
   
    public class Customer
    {
       
        public Int64 CustomerID { get; set; }
        public string CustNo { get; set; }
        public Int64 UBAccountNo { get; set; }
        public string PhoneNumber { get; set; }
        public Int64? DealerID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PrimaryContactName { get; set; }
        public string Position { get; set; }
        public string ContactNo { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime DOB { get; set; }
        public Int64? PlanNo { get; set; }
        public Int64? BillRunCycleNumber { get; set; }
        public string Contract_start { get; set; }
        public string Contract_end { get; set; }
        public string Contract_Term { get; set; }
        public string GroupNo { get; set; }
        public string EntityType { get; set; }

        public Int64? AddressID { get; set; }
        public virtual List<Address> AddressDetails { get; set; }

        public Int64? CompanyID { get; set; }
        public virtual Company CompanyDetails { get; set; }
        public string Salutation { get; set; }



    }

    public enum EntityType
    {
        Proprietor,
        SoleTrader,
        Consumer,
        RegisteredBusinessEntityOther
    }

}
