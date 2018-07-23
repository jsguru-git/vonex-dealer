using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository
{
    public interface ICustomerRepository
    {
        Task<Address> AddAddress(Address address);
        Task<Address> UpdateAddress(Address address);
        IQueryable<Customer> GetCustomers(Int64 CustomerID = 0);
        Task<Customer> GetCustomerAsync(Int64 CustomerID);
        Task<Address> GetAddressAsync(Int64 addressID);
        Task<List<Address>> GetAllCustomerAddressesAsync(Int64 customerID);
        Task<List<Customer>> getCustomersByDealerAsync(Int64 dealerID);
        Task<List<Customer>> getCustomersAsync(string firstname = null, string lastname = null , string businessName= null, Int64 dealerID = 0);
        Task<Customer> UpdateCustomerAsync(Int64 customerID, Customer customer);
        Task<bool> DeleteCustomerAsync(Int64 customerID);
        Task<Customer> CreateCustomerAsync(Customer cust);
        Task<Address> AddAddressToCustomerAsync(Int64 custID, Address address);
        Task<Address> UpdateAddressToCustomerAsync(Int64 custID, Address address);
        Task<List<Dealer>> getDealersAsync(Int64 dealerID = 0);
        Task<Dealer> CreateDealerAsync(Dealer dealer);
        Task<Dealer> UpdateDealerAsync(Int64 dealerID, Dealer dealer);
        Task<bool> DeleteDealerAsync(Int64 dealerID);
        Task<Customer> addCustomerToUBAsync(Customer cust);
    }
}