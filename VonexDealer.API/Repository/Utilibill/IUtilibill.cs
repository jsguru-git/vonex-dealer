using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using wsUtbCustomer;

namespace VonexDealer.API.Repository
{
    /// <summary>
    /// Interface to Utilibill
    /// this interface provides ability to log into UB
    /// create a customer
    /// retrieve a cunstomer
    /// add direct debit info
    /// 
    /// </summary>
    public interface IUtilibill
    {
        Task<CustomerResponse> GetCustomerAsync(string custNo);
        Task<CustomerResponse> AddCustomerAsync(Customer customer);
        Task<CustomerResponse> EditCustomerAsync(wsCustomer customer);
    }
}
