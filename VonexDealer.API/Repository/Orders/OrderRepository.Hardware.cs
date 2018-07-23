using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {

        public async Task<List<Hardware>> getHandsetsOffOrder(Int64 orderID)
        {
            var extensions = await _context.IPExtensions
                .Join(_context.Handsets, x => x.HandsetID, y => y.HandsetID, (x, y) => new
                HardwareCount
                {
                    OrderID = x.OrderID ?? 0,
                    HandsetID = x.HandsetID,
                    Cost = y.RRPEx,
                    DealerSupplied = x.DealerSupplied

                })
                .Where(x => x.OrderID == orderID && x.DealerSupplied == false)
                .GroupBy(x => x.HandsetID)
                .Select(g => new HardwareCountSummary
                {
                    Quantity = g.Count(),
                    HandsetID = g.Key,

                })
                .ToListAsync();




            var summary = extensions.Join(_context.Handsets, x => x.HandsetID, y => y.HandsetID,
                (x, y) => new HardwareCountSummary
                {
                    HandsetID = x.HandsetID,
                    DealerSupplied = false,
                    Cost = y.RRPEx,
                    Quantity = x.Quantity,


                }).ToList();


            Order order = _context.Orders.Where(x => x.OrderID == orderID).FirstOrDefault();

            List<Hardware> alreadySaved = await _context.Hardware.Where(x => x.HardwareOrderID == order.HardwareID && x.FromIPOrder == true).AsNoTracking().ToListAsync();
            if (alreadySaved.Count > 0)
            {
                _context.Hardware
                    .RemoveRange(alreadySaved);
                await _context.SaveChangesAsync();
            }

            List<Hardware> ipHardware = new List<Hardware>();


            foreach (var item in summary)
            {

                ipHardware.Add(new Hardware
                {
                    HardwareOrderID = order.HardwareID == 0 ? 0 : order.HardwareID,
                    HandsetID = item.HandsetID,
                    Quantity = item.Quantity,
                    Cost = item.Cost,
                    LineTotal = item.Cost * item.Quantity,
                    FromIPOrder = true,

                });
            }
            await AddHardwareToOrderAsync(order.HardwareID, ipHardware);

            return ipHardware;



        }

        public async Task<HardwareOrder> CreateHardwareOrderAsync(HardwareOrder hardware)
        {

            _context.HardwareOrders.Add(hardware);

            Order order = await _context.Orders.FindAsync(hardware.OrderID);

            try
            {
                await _context.SaveChangesAsync();
                if (order != null)
                    order.HardwareID = hardware.HardwareOrderID;
                await _context.SaveChangesAsync();



                return hardware;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

        }
        public async Task<HardwareOrder> UpdateHardwareOrderAsync(HardwareOrder hardware)
        {
            _context.HardwareOrders.Update(hardware);
            try
            {
                await _context.SaveChangesAsync();
                return hardware;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
        public async Task<HardwareOrder> DeleteHardwareOrderAsync(HardwareOrder hardware)
        {
            _context.HardwareOrders.Remove(hardware);
            try
            {
                await _context.SaveChangesAsync();
                return hardware;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
        public async Task<List<Hardware>> AddHardwareToOrderAsync(Int64 hardwareOrderID, List<Hardware> hardware)
        {
            if (hardwareOrderID == 0)
                return null;

            foreach (Hardware item in hardware)
            {
                item.HardwareOrderID = hardwareOrderID;
                if (item.HardwareID > 0)
                {
                    _context.Hardware.Update(item);
                }
                else
                {
                    _context.Hardware.Add(item);
                }

            }

            try
            {
                await _context.SaveChangesAsync();
                return hardware;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }
        }

        public async Task<bool> RemoveHardwareOnOrderAsync(Int64 orderID, List<Hardware> hardware)
        {
            if (orderID == 0)
            {
                return false;
            }

            foreach (var item in hardware)
            {
                if (item.HardwareID > 0)
                {
                    _context.Hardware.Remove(item);
                }
            }

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
        public async Task<bool> DeleteHardwareOnOrderAsync(Int64 hardwareOrderID)
        {

            if (hardwareOrderID == 0)
                return false;

            var order = await _context.HardwareOrders.FindAsync(hardwareOrderID);

            if (order == null)
                return false;

            _context.HardwareOrders.Remove(order);

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

        public async Task<HardwareOrder> GetHardwareOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
                return null;

            HardwareOrder order = await _context.HardwareOrders
                .Where(x => x.OrderID == orderID)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return order;
        }


        public async Task<List<Hardware>> GetHardwareOnOrderAsync(Int64 orderID, bool OnlyIPVoice = false)
        {
            if (orderID == 0)
                return null;
            HardwareOrder hardwareOrder = _context.HardwareOrders.Where(x => x.OrderID == orderID).FirstOrDefault();

            // Order order = _context.Orders.Where(x => x.OrderID == orderID).FirstOrDefault();
            if (hardwareOrder == null)
            {
                hardwareOrder = await CreateHardwareOrderAsync(new HardwareOrder { OrderID = orderID });
            }
            List<Hardware> orders = await getHandsetsOffOrder(orderID);


            orders = await _context.Hardware
                  .Where(x => x.HardwareOrderID == hardwareOrder.HardwareOrderID)
                  .Where(x => OnlyIPVoice ? x.FromIPOrder == OnlyIPVoice : 1 == 1)
                  .AsNoTracking()
                  .ToListAsync();

            return orders;
        }

    }
}
