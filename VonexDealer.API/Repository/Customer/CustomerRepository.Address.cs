using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository
{
    public partial class CustomerRepository
    {

        public async Task<Address> AddAddress(Address address)
        {
            if (address == null)
                return null;
            if (address.StreetAddress.IndexOf(',') > 0)
            {
                address.StreetAddress = address.StreetAddress.Substring(0, address.StreetAddress.IndexOf(','));
            }

            if (address.AddressID == 0)
                _context.Entry(address).State = EntityState.Added;
            else
                _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            return address;
        }

        public async Task<Address> UpdateAddress(Address address)
        {
            if (address == null)
            {
                _logger.LogError($"Update Address - {address} has no address");
                return null;
            }


            try
            {

                _context.Addresses.Update(address);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Error updating Address {ex.Message}");
                return null;
            }
            return address;
        }




        /// <summary>
        /// add's address to customer.
        /// </summary>
        /// <param name="custID"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<Address> AddAddressToCustomerAsync(long custID, Address address)
        {

            if (address == null || custID == 0)
                return null;

            if (address.AddressID == 0)
            {
                await AddAddress(address);
            }
            else
            {
                await UpdateAddress(address);
            }

            if (custID > 0)
            {
                Customer cust = await GetCustomerAsync(custID);

                cust.AddressID = address.AddressID;

                _context.Entry(cust).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }

                catch (DbException ex)
                {
                    _logger.LogError(ex.Message);
                    return null;
                }
                return address;

            }
            return null;
        }

        public async Task<Address> UpdateAddressToCustomerAsync(long custID, Address address)
        {
            Customer cust = await _context.Customers.FindAsync(custID); //GetCustomers(custID).AsNoTracking().FirstOrDefault();
            if (cust == null)
            {
                _logger.LogError($"Update customer Address - customer {custID} unknown");
                return null;
            }

            if (address.AddressID == 0)
            {
                _logger.LogError($"Update customer Address - addressID {address.AddressID} unknown");
                return null;
            }

            cust.AddressID = address.AddressID;
            _context.Entry(cust).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Error updating Address {ex.Message}");
                return null;
            }
            return address;
        }

    }

}

