using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Data;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository
{
    public partial class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _context;
        private ILogger<CustomerRepository> _logger;
        private IConfiguration _config;
        private IUtilibill _utilibill;

        public CustomerRepository(ApplicationDbContext context,
            ILogger<CustomerRepository> logger,
            IConfiguration config,
            IUtilibill utilibill)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _utilibill = utilibill;
        }


        public async Task<Customer> GetCustomerAsync(Int64 CustomerID)
        {
            if (CustomerID == 0)
                _logger.LogError("No customer ID provided");

            Customer customer = await _context.Customers
                .Include(x => x.CompanyDetails)
                .Include(x => x.AddressDetails)
                .Where(x => CustomerID == 0 || x.CustomerID == CustomerID)
              
                // .AsNoTracking()
                .FirstOrDefaultAsync();

            return customer;

        }

        public async Task<Address> GetAddressAsync(Int64 addressID)
        {
            if (addressID == 0)
                _logger.LogError("No address ID provided");

            Address address = await _context.Addresses
                .Where(x => x.AddressID == addressID)
                // .AsNoTracking()
                .FirstOrDefaultAsync();

            return address;

        }

        public async Task<List<Address>> GetAllCustomerAddressesAsync(Int64 customerID)
        {
            if (customerID == 0)
                _logger.LogError("No customer ID provided");

            List<Address> addresses = await _context.Addresses
                .Where(x => x.CustomerID == customerID)
                .ToListAsync();

            return addresses;

        }

        public IQueryable<Customer> GetCustomers(Int64 CustomerID = 0)
        {
            if (CustomerID == 0)
                _logger.LogError("No customer ID provided");

            return _context.Customers
                .Include(x => x.CompanyDetails)
                .Include(x => x.AddressDetails)
                .Where(x => CustomerID == 0 || x.CustomerID == CustomerID)
                //  .AsNoTracking()
                .AsQueryable();

        }


        public async Task<List<Customer>> getCustomersByDealerAsync(Int64 dealerID)
        {
            List<Customer> customers = await _context.Customers
                .Include(x => x.CompanyDetails)
                .Include(x => x.AddressDetails)
                .Where(x => x.DealerID == dealerID)
                .AsNoTracking()
                .ToListAsync();

            return customers;

        }

        public async Task<List<Customer>> getCustomersAsync(string firstname = null, string lastname = null, string businessName = null, Int64 dealerID = 0)
        {

            int maxCount = int.Parse(_config["maxcount"]);
            return await _context.Customers
            .Include(x => x.CompanyDetails)
            .Include(x => x.AddressDetails)
                .Where(x => firstname == null || x.FirstName == firstname)
                .Where(x => lastname == null || x.Surname == lastname)
                .Where(x => businessName == null || x.CompanyDetails.CompanyName == businessName)
                .Where(x => dealerID == 0 || x.DealerID == dealerID)
                .AsNoTracking()
                .Take(maxCount)
                .ToListAsync();

        }

        /// <summary>
        /// Add a validated customer to UB
        /// </summary>
        /// <param name="cust"></param>
        /// <returns></returns>
        public async Task<Customer> addCustomerToUBAsync(Customer cust)
        {
            try
            {
                List<Address> address = await _context.Addresses.Where(x => x.CustomerID == cust.CustomerID).ToListAsync();
                if (address != null)
                {
                    cust.AddressDetails = address;
                }
                else
                    return null;

                CustomerResponse response = await _utilibill.AddCustomerAsync(cust);
                if (response.customer != null)
                {
                    cust.UBAccountNo = Convert.ToInt64(response.customer.custNo);
                    await UpdateCustomerAsync(cust.CustomerID, cust);
                    return cust;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return null;
            }

        }


        public async Task<Customer> CreateCustomerAsync(Customer cust)
        {

            try
            {
                if(cust.CompanyID == 0)
                {
                    cust.CompanyID = null;
                }
                _context.Customers.Add(cust);
                await _context.SaveChangesAsync();


                return cust;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"CreateCustomerAsync: {ex.InnerException}");
                return null;
            }

        }
        /// <summary>
        /// Update base Customer record details only,
        /// if connected records to be updated, they need to be called separately.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> UpdateCustomerAsync(Int64 customerID, Customer customer)
        {
            //Customer cust = GetCustomers(customerID).FirstOrDefault();
            if (customer == null || customer.CustomerID == 0)
            {
                _logger.LogError($"UpdateCustomerAsync - Customer does not exist {customerID}");
                return null;
            }

            //todo: return null doesn't give any feedback??
            try
            {
                //cust.CustNo = customer.CustNo;
                //cust.DateAdded = customer.DateAdded;
                //cust.DealerID = customer.DealerID;
                //cust.Email = customer.Email;
                //cust.FirstName = customer.FirstName;
                //cust.LastName = customer.LastName;
                //cust.PhoneNumber = customer.PhoneNumber;
                //cust.UBAccountNumber = customer.UBAccountNumber;
                if(customer.CompanyDetails == null)
                {
                    customer.CompanyDetails = new Company();
                }
                _context.Customers.Update(customer);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"UpdateCustomerAsync: {ex.InnerException.ToString()}");
                return null;
            }

            return customer;


        }

        public async Task<bool> DeleteCustomerAsync(Int64 customerID)
        {
            Customer custs = await _context.Customers
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.CustomerID == customerID);
            if (custs == null)
            {
                return false;
            }

            try
            {
                _context.Customers.Remove(custs);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                _logger.LogError($"DeleteCustomerAsync: Error {ex.Message}");
                return false;
            }
        }

        public async Task<List<Dealer>> getDealersAsync(long dealerID = 0)
        {
            _logger.LogInformation($"getDealers dealerID = {dealerID}");
            List<Dealer> dealers = await _context.Dealers
                .Where(x => dealerID == 0 || x.DealerID == dealerID)
                .ToListAsync();

            return dealers;

        }

        public async Task<Dealer> CreateDealerAsync(Dealer dealer)
        {
            _logger.LogInformation("Create Dealer {Dealer}", dealer);
            if (dealer.DealerID > 0)
            {
                dealer.DealerID = -1;
            }
            _context.Dealers.Add(dealer);
            _context.Entry(dealer).State = EntityState.Added;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                _logger.LogError("Create Dealer failed {DbUpdateException}", ex);

            }

            return dealer;

        }

        public async Task<Dealer> UpdateDealerAsync(Int64 dealerID, Dealer dealer)
        {
            _logger.LogInformation("Update Dealer {Dealer}", dealer);
            List<Dealer> updatedealers = await getDealersAsync(dealerID);

            if (updatedealers != null)
            {
                updatedealers[0] = dealer;
                _context.Entry(updatedealers[0]).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("UpdateDealerAsync {DbUpdateException}", ex);
                return dealer;
            }

            return dealer;

        }

        public async Task<bool> DeleteDealerAsync(Int64 dealerID)
        {
            _logger.LogInformation($"Delete Dealer {dealerID}");

            Dealer dealer = await _context.Dealers
                .Where(x => dealerID == 0 || x.DealerID == dealerID)
                .FirstOrDefaultAsync();
            _context.Entry(dealer).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                _logger.LogError("DeleteDealerAsync {DbUpdateException}", ex);
                return false;
            }

            return true;

        }

        public IQueryable<Order> GetOrders(Int64 orderID = 0)
        {
            return _context.Orders
                .Include(x => x.CustomerDetails)
                .Include(x => x.CustomerDetails.CompanyDetails)
                //.Include(x => x.DirectDebitDetails)
                //.Include(x => x.HardwareDetails)
                //.Include(x => x.IPOrderDetails)
                //.Include(x => x.LandlineDetails)
                //.Include(x => x.InternetDetails)
                .Where(x => orderID == 0 || x.OrderID == orderID)
                .AsQueryable();
        }

        public IQueryable<Internet> GetInternet(Int64 internetID = 0)
        {
            return _context.Internet.Where(x => internetID == 0 || x.InternetID == internetID);
        }

        public async Task<Order> RemoveCustomerFromOrderAsync(long orderID)
        {

            Order order = GetOrders(orderID).FirstOrDefault();

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return order;
            }
            else
                return null;

        }

        public async Task<Order> RemoveIPOrderFromOrderAsync(long orderID)
        {

            Order order = GetOrders(orderID).FirstOrDefault();

            if (order != null)
            {
                order.IPOrderID = null;
                _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return order;
            }
            else
                return null;

        }

        public IQueryable<IPOrder> GetIPOrders(Int64 IpOrderID = 0)
        {
            return _context.IPOrders.Where(x => IpOrderID == 0 || x.IPOrderID == IpOrderID).AsQueryable();
        }

        public async Task<Order> UpdateIPOrderToOrderAsync(long orderID, long IPOrderId)
        {

            Order order = GetOrders(orderID).FirstOrDefault();

            if (order != null)
            {
                IPOrder iporder = GetIPOrders(IPOrderId).FirstOrDefault();
                if (iporder != null)
                {
                    order.IPOrderID = IPOrderId;
                    _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return order;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;

        }

        public async Task<Order> RemoveInternetFromOrderAsync(long orderID)
        {

            Order order = GetOrders(orderID).FirstOrDefault();

            if (order != null)
            {
                order.InternetID = null;
                _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return order;
            }
            else
                return null;

        }

        public async Task<Internet> AddInternet(Internet internet)
        {
            _context.Internet.Add(internet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError($"unable to create internet {ex.InnerException}");
                return null;
            }
            return internet;
        }

        public async Task<Order> AddInternetToOrderAsync(long orderID, Internet internet)
        {
            if (internet == null || orderID == 0)
            {
                _logger.LogError($"UpdateInternetAsync - Internet does not exist");
                return null;
            }
            Order order = GetOrders(orderID).FirstOrDefault();
            if (order != null)
            {
                internet = await AddInternet(internet);
                order.InternetID = internet.InternetID;
                _context.Update(order);
                await _context.SaveChangesAsync();
                return order;
            }
            else
                return null;
        }

        public async Task<Order> UpdateInternetToOrderAsync(long orderID, long internetID)
        {

            Order order = GetOrders(orderID).FirstOrDefault();

            if (order != null)
            {
                order.InternetID = internetID;
                _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return order;
            }
            else
                return null;

        }

        public async Task<Order> RemoveLandLineFromOrderAsync(long orderID)
        {

            Order order = GetOrders(orderID).FirstOrDefault();

            if (order != null)
            {
                order.LandlineID = null;
                _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return order;
            }
            else
                return null;

        }

        public async Task<Order> UpdateLandLineToOrderAsync(long orderID, long landLineID)
        {

            Order order = GetOrders(orderID).FirstOrDefault();

            if (order != null)
            {
                order.LandlineID = landLineID;
                _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return order;
            }
            else
                return null;

        }
    }
}
