using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public partial class Company
    {
        public Int64 CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string TradingName { get; set; }
        public string EntityType { get; set; }
        public string DirectorGivenName { get; set; }
        public string DirectorSurname { get; set; }
        public string ABN { get; set; }
        public string ACN { get; set; }
        public string DriversLicenseNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        [NotMapped]
        public DateTime DobConsumer { get; set; }
        [NotMapped]
        public bool guaranteeAttached { get; set; }
        
        public bool isdateOfBirthRequired { get; set; }
       
        public bool isdirectorSurnameRequried { get; set; }
        [NotMapped]
        public bool otherOrganisation { get; set; }
        //public Int64? CustomerID { get; set; }
    }
}
