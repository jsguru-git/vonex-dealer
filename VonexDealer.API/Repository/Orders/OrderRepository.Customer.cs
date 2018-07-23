using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {

        public async Task<List<Address>> GetAddressesOnOrderAsync(Int64 orderID)
        {

            Order order = await _context.Orders.Where(x => x.OrderID == orderID).FirstOrDefaultAsync();
            if (order == null)
                return null;

            Int64 customerID = order.CustomerID ;

            List<Address> addresses = _context.Addresses.Where(x => x.CustomerID == customerID).ToList();
            return addresses;

        }

        public async Task<Customer> AddCustomerToOrderAsync(long orderID, Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            Order order = GetOrders(orderID).FirstOrDefault();
            if (order != null)
            {
                order.CustomerID = customer.CustomerID;
                _context.Update(order);
                await _context.SaveChangesAsync();

            }

            return customer;
        }
        public Task<bool> RemoveCustomerFromOrderAsync(long orderID, long customerID)
        {
            throw new NotImplementedException();
        }
        public Task<Customer> UpdateCustomerOnOrderAsync(long orderID, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
