using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Data;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository
{
    public class LandlineRepository : ILandline
    {
        private ApplicationDbContext _context;
        private ILogger<LandlineRepository> _logger;

        public LandlineRepository(ApplicationDbContext context, ILogger<LandlineRepository> logger)
        {
            _context = context;
            _logger = logger;

        }
        public async Task<Churn> AddChurnAsync(Churn churn)
        {
           if(churn == null)
            {
                return null;
            }

            if (churn.ChurnID > 0)
            {
                //needs to be an update not add
                return await UpdateChurnAsync(churn.ChurnID, churn);
            }

            _context.Churns.Add(churn);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }
            return churn;
        }

        public async Task<ISDN> AddISDNAsync(ISDN isdn)
        {
            if (isdn  == null)
            {
                return null;
            }

            if (isdn.ISDNID > 0)
            {
                await UpdateISDNAsync(isdn.ISDNID, isdn);
            }
            else
            {
                _context.ISDNs.Add(isdn);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbException ex)
                {
                    _logger.LogError(ex.InnerException.ToString());
                    return null;
                }
            }
            return isdn ;
        }

        public async Task<Landline> AddLandlineAsync(Landline landline)
        {
            if (landline == null)
            {
                return null;
            }

            _context.Landlines.Add(landline);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

            if(landline.OrderID > 0)
            {

            }
            return landline;
        }

        public async Task<NewPSTN> AddNewPSTNAsync(NewPSTN newPSTN)
        {
            if (newPSTN == null)
            {
                return null;
            }
            if(newPSTN.NewPSTNID > 0)
            {
                _context.Update(newPSTN);
            }
            else
            {

            _context.NewPSTNs.Add(newPSTN);
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
            return newPSTN;
        }

        public async Task<List<Churn>> GetChurnsOnOrder(Int64 orderID)
        {
            if (orderID == 0)
                return null;

            Order order = await _context.Orders.FindAsync(orderID);
            if (order == null)
                return null;

            Int64 landlineID = order.LandlineID ?? 0;



            List<Churn> churns = await _context.Churns
                .Where(x => x.LandlineID == landlineID)
                .ToListAsync();
            Int64 customerID = order.CustomerID;

            List<Address> addresses = await _context.Addresses
                .Where(x => x.CustomerID == customerID)
                .ToListAsync();

            var joinedData = from c in churns
                             join a in addresses on c.AddressID equals a.AddressID
                             select new { c, a };

            foreach (var churn in joinedData)
            {
                churn.c.FormattedAddress = churn.a.FormattedAddress;
            }

            return joinedData.Select(x => x.c).ToList();
        }

        public async Task<List<ISDN>> GetISDNsOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
                return null;

            Landline result = await GetLandlinesOnOrder(orderID);

            if (result == null)
                return null;

            Int64 landlineID = result.LandlineID;

            return await _context.ISDNs.Where(x => x.LandlineID == landlineID).ToListAsync();
        }

        public async Task<Landline> GetLandlinesOnOrder(Int64 orderID)
        {
            if (orderID == 0)
                return null;

            Landline result = await _context.Landlines.Where(x => x.OrderID == orderID).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<NewPSTN>> GetNewPSTNsOnOrder(Int64 orderID)
        {
            if (orderID == 0)
                return null;
            Landline landline = await GetLandlinesOnOrder(orderID);
            if (landline != null)
            {
                return await _context.NewPSTNs.Where(x => x.LandlineID == landline.LandlineID).ToListAsync();
            }
            return null;
        }

        public async Task<bool> RemoveChurnAsync(Int64 churnID)
        {
            Churn churn = await _context.Churns.FindAsync(churnID);

            if (churn != null)
            {
               
                _context.Entry(churn).State = EntityState.Deleted;
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
            else
                return false;

        }

        public async Task<bool> RemoveISDNAsync(Int64 isdnID)
        {
            ISDN isdn = await _context.ISDNs.FindAsync(isdnID);

            if (isdn != null)
            {

                _context.Entry(isdn).State = EntityState.Deleted;
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
            else
                return false;
        }

        public async Task<bool> RemoveLandlineAsync(Int64 landlineID)
        {
            Landline landline = await _context.Landlines.FindAsync(landlineID);

            if (landline != null)
            {

                _context.Entry(landline).State = EntityState.Deleted;
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
            else
                return false;
        }

        public async Task<bool> RemoveNewPSTNAsync(Int64 newPSTNID)
        {
            NewPSTN pstn = await _context.NewPSTNs.FindAsync(newPSTNID);

            if (pstn != null)
            {

                _context.Entry(pstn).State = EntityState.Deleted;
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
            else
                return false;
        }

        public async Task<Churn> UpdateChurnAsync(Int64 churnID, Churn churn)
        {
            if (churn == null || churnID == 0 || churn.ChurnID == 0)
            {
                _logger.LogError($"UpdateChurnAsync - churn does not exist");
                return null;
            }

            try
            {
                    _context.Churns.Update(churn);

                    await _context.SaveChangesAsync();
                    return churn;
            }
            catch (DbUpdateException ex)
            {

                _logger.LogError($"UpdateChurnAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<ISDN> UpdateISDNAsync(Int64 isdnID, ISDN isdn)
        {
            if (isdn == null || isdnID == 0 || isdn.ISDNID == 0)
            {
                _logger.LogError($"UpdateISDNAsync - isdn does not exist");
                return null;
            }
            try
            {
                _context.ISDNs.Update(isdn);

                await _context.SaveChangesAsync();
                return isdn;
            }
            catch (DbUpdateException ex)
            {

                _logger.LogError($"UpdateISDNAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<Landline> UpdateLandlineAsync(long landlineID, Landline landline)
        {
            if (landline == null || landlineID == 0 || landline.LandlineID == 0)
            {
                _logger.LogError($"UpdateLandlineAsync - landline does not exist");
                return null;
            }
            try
            {
                _context.Landlines.Update(landline);

                await _context.SaveChangesAsync();
                return landline;
            }
            catch (DbUpdateException ex)
            {

                _logger.LogError($"UpdateLandlineAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<NewPSTN> UpdateNewPSTNAsync(Int64 newPSTNID, NewPSTN newPSTN)
        {
            if (newPSTN == null || newPSTNID == 0 || newPSTN.NewPSTNID == 0)
            {
                _logger.LogError($"UpdateNewPSTNAsync - newPSTN does not exist");
                return null;
            }
            try
            {
                _context.NewPSTNs.Update(newPSTN);

                await _context.SaveChangesAsync();
                return newPSTN;
            }
            catch (DbUpdateException ex)
            {

                _logger.LogError($"UpdateNewPSTNAsync: {ex.Message}");
                return null;
            }
        }
    }
}
