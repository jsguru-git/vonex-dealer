using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VonexDealer.API.Data;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;
        private ILogger<OrderRepository> _logger;
        private IHostingEnvironment _env;
        private IConfiguration _config;
        private ILandline _landline;

        public OrderRepository(ApplicationDbContext context, ILogger<OrderRepository> logger, IHostingEnvironment environment, IConfiguration config, ILandline landline)
        {
            _context = context;
            _logger = logger;
            _env = environment;
            _config = config;
            _landline = landline;
        }



        /// <summary>
        /// get the dealerID of the logged in user.
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        public Int64 loggedInDealerID(ClaimsPrincipal dealer)
        {
            string dealerEmail = "";


            dealerEmail = dealer.Claims.Where(x => x.Type == "name")
                    .Select(x => x.Value)
                    .FirstOrDefault();

            Int64 dealerID = 0;
            if (dealerEmail.Length > 0)
            {
                dealerID = _context.Dealers.Where(x => x.DealerEmail == dealerEmail).Select(x => x.DealerID).FirstOrDefault();
            }
            return dealerID;
        }
        /// <summary>
        /// Loggged in dealer email.
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        public string loggedInDealerEmail(ClaimsPrincipal dealer)
        {
            string dealerEmail = "";

            dealerEmail = dealer.Claims.Where(x => x.Type == "name")
                    .Select(x => x.Value)
                    .FirstOrDefault();
            return dealerEmail;
        }

        public async Task<bool> UpdateSignature(Guid orderID, string signature)
        {
            if (orderID == Guid.Empty)
                return false;

            Order order = await _context.Orders.Where(x => x.OrderGUID == orderID).FirstOrDefaultAsync();

            if (order == null)
                return false;

            order.Signature = signature;
            order.OrderStatus = (int)Status.Signed;
            order.OrderGUID = new Guid();
            _context.Entry(order).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateOrderStatus(Int64 orderID, int status)
        {
            Order order = await _context.Orders.Where(x => x.OrderID == orderID).FirstOrDefaultAsync();

            if (order == null)
                return false;

            order.OrderStatus = status;

            _context.Entry(order).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return false;
            }
        }

        public Task<Order> CancelOrderAsync(Int64 orderID)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            Int64 dealerID = order.DealerID;
            if (dealerID == 0)
            {
                //get dealer from customer.
                Customer customer = _context.Customers.Find(order.CustomerID);
                if (customer == null)
                    return null;
                dealerID = customer.DealerID ?? 0;
                order.DealerID = dealerID;
            }
                if (order.OrderGUID == Guid.Empty) order.OrderGUID = Guid.NewGuid();

            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Unable to create order {DbUpdateException}", ex);
                return null;
            }
            return order;
        }


        public async Task<Order> UpdateOrderAsync(Int64 orderID, Order order)
        {
            Order updateOrder = GetOrders(orderID).FirstOrDefault();
            if (updateOrder != null)
            {
                updateOrder = order;
                _context.Entry(updateOrder).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return order;
            }
            else
                return null;

        }



        public IQueryable<Order> GetOrders(Int64 orderID = 0, string dealerEmail = "")
        {
            Int64 dealerID = 0;
            if (dealerEmail != null)
            {
                dealerID = _context.Dealers.Where(x => x.DealerEmail == dealerEmail).Select(x => x.DealerID).FirstOrDefault();
            }

            return _context.Orders
                .Include(x => x.CustomerDetails)
                .ThenInclude(x => x.AddressDetails)
                .Include(x => x.CustomerDetails.CompanyDetails)
                //.Include(x => x.DirectDebitDetails)
                //.Include(x => x.HardwareDetails)
                //.Include(x => x.IPOrderDetails)
                //.Include(x => x.LandlineDetails)
                //.Include(x => x.InternetDetails)
                .Where(x => orderID == 0 || x.OrderID == orderID)
                .Where(x => dealerID == 0 || x.DealerID == dealerID)
                .AsQueryable();
        }

        public IQueryable<IPOrder> GetIPOrders(Int64 IpOrderID = 0)
        {
            return _context.IPOrders.Where(x => IpOrderID == 0 || x.IPOrderID == IpOrderID).AsQueryable();
        }

        public IQueryable<RatePlan> GetRatePlans(Int64 IpOrderID = 0)
        {
            return _context.RatePlans.AsQueryable();
        }

        public async Task<IPOrder> CreateIPOrderAsync(IPOrder iporder)
        {
            _context.IPOrders.Add(iporder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Create IPOrder failed {DbUpdateException}", ex);
            }
            return iporder;
        }

        public string GetApplicationUrl()
        {
            return _config["FrontEnd"].ToString();
        }
    }
}
