using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {

        public async Task<IPOrder> GetIpOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
                return null;

            IPOrder order = await _context.IPOrders.Where(x => x.OrderID == orderID).FirstOrDefaultAsync();
            return order;


        }

        public async Task<List<IPExtension>> GetIPExtensionsOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
                return null;

            List<IPExtension> extensions =  await _context.IPExtensions.Where(x => x.OrderID == orderID).ToListAsync();
            return extensions;

        }


        public async Task<IPOrder> AddIPOrderToOrderAsync(Int64 orderID, IPOrder IPOrder)
        {
            if (orderID == 0)
                return null;
            IPOrder.OrderID = orderID;

            Order order = await GetOrders(orderID).FirstOrDefaultAsync();

            if (order == null)
                return null;


            if (IPOrder.IPOrderID > 0)
            {
                _context.IPOrders.Update(IPOrder);
            }
            else
            {
                _context.IPOrders.Add(IPOrder);

            }

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

            order.IPOrderID = IPOrder.IPOrderID;
            _context.Orders.Update(order);

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

            return IPOrder;

        }

        public async Task<List<IPExtension>> AddIPExtensionsToOrderAsync(long orderID, List<IPExtension> extensions)
        {

            if (orderID == 0 || extensions.Count == 0)
                return null;

            foreach (IPExtension ext in extensions)
            {
                ext.OrderID = orderID;
                if (ext.IPExtensionID > 0)
                {
                    _context.IPExtensions.Update(ext);
                }
                else
                {
                    _context.IPExtensions.Add(ext);
                }

            }
            try
            {
                await _context.SaveChangesAsync();
                return extensions;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

        }

        public async Task<IPExtension[]> RemoveIPExtensionsAsync(long orderID, IPExtension[] extensions)
        {

            if (orderID == 0 || extensions.Length == 0)
                return null;

            foreach (IPExtension ext in extensions)
            {
                var extension = _context.IPExtensions.Find(ext.IPExtensionID);

                if (extension != null)
                {
                    _context.Entry(extension).State = EntityState.Deleted;
                }

            }
            try
            {
                await _context.SaveChangesAsync();
                return extensions;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

        }


        public Task<bool> RemoveIPOrderFromOrderAsync(long orderID, long IPOrderID)
        {
            throw new NotImplementedException();
        }
        public Task<IPOrder> UpdateIPOrderOnOrderAsync(long orderID, IPOrder IPOrder)
        {
            throw new NotImplementedException();
        }
    }
}
