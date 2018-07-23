using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    /// <summary>
    /// Storage of customer bank details may beed to be altered.
    /// This should only be a temporary store that will transfer to UB for proper storage.
    /// </summary>
    public class DirectDebit
    {
        public Int64 DirectDebitID { get; set; }
        public string AccountName { get; set; }
        public string BSB { get; set; }
        public string AccountNumber { get; set; }
        public string FinancialInstitutionName { get; set; }
        public string FinancialInstitutionBranch { get; set; }
    }
}
