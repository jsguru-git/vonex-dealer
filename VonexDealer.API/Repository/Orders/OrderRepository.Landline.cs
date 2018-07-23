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
        public async Task<Landline> AddLandlineToOrderAsync(long orderID, Landline landline)
        {
            Order order = await _context.Orders.FindAsync(orderID);
            if (order == null)
                return null;

            order.LandlineID = landline.LandlineID;
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

            return landline;

        }

        public async Task<bool> RemoveLandlineFromOrderAsync(long orderID, long landlineID)
        {
            Order order = await _context.Orders.FindAsync(orderID);
            if (order == null)
                return false;

            order.LandlineID = 0;
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return false;
            }

            return true;

        }

        public async Task<Churn[]> RemovePSTNChurnFromOrderAsync(long orderID, Churn[] churns)
        {

            if (orderID == 0)
                return null;


            foreach (Churn churn in churns)
            {
                Churn delchurn = await _context.Churns.FindAsync(churn.ChurnID);
                _context.Entry(delchurn).State = EntityState.Deleted;
            }
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

            return churns;

        }

        public async Task<NewPSTN[]> RemoveNewPSTNFromOrderAsync(long orderID, NewPSTN[] pstns)
        {

            if (orderID == 0)
                return null;


            foreach (NewPSTN pstn in pstns)
            {
                NewPSTN delpstn = await _context.NewPSTNs.FindAsync(pstn.NewPSTNID);
                _context.Entry(delpstn).State = EntityState.Deleted;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

            return pstns;

        }

        public async Task<Landline> UpdateLandlineOnOrderAsync(long orderID, Landline landline)
        {
            Order order = await _context.Orders.FindAsync(orderID);
            if (order == null)
                return null;

            order.LandlineID = landline.LandlineID;
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

            return landline;

        }

    }
}
