using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {
        public async Task<Dealer> getDealerAsync(Int64 dealerID)
        {
            Dealer dealer = await _context.Dealers
                .Where(x => x.DealerID == dealerID)
                .FirstOrDefaultAsync();

            if (dealer != null)
            {
                return dealer;
            }
            else
                return null;

        }

       
        public async Task<Dealer> updateDealer(Int64 dealerID, Dealer dealer)
        {
            //_context.Dealers.Update(dealer);
            try
            {

                _context.Dealers.Update(dealer);
                await _context.SaveChangesAsync();

                //var getDealer = await getDealerAsync(dealerID);
                //if (getDealer == null)
                //{
                //    return null;
                //}
                //else
                //{
                //    getDealer.DealerEmail = dealer.DealerEmail;
                //    getDealer.DealerFullName = dealer.DealerFullName;
                //    getDealer.UBDealerID = dealer.UBDealerID;
                //    _context.Entry(getDealer).State = EntityState.Modified;
                //    await _context.SaveChangesAsync();
                //}

            }
            catch (DbException ex)
            {
                _logger.LogError($"UpdateDealerAsync: {ex.Message}");
                return null;
            }
            return dealer;
        }
       
        public async Task<Dealer> addDealer(Dealer dealer)
        {
            _context.Dealers.Add(dealer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError($"unable to create dealer {ex.InnerException}");
                return null;
            }
            return dealer;
        }
       
        public async Task<bool> removeDealer(Int64 dealerID)
        {
            Dealer dealer = await _context.Dealers
                .SingleOrDefaultAsync(m => m.DealerID == dealerID);
            if (dealer == null)
            {
                return false;
            }
            else
            {
                _context.Dealers.Remove(dealer);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public IQueryable<Dealer> getDealers()
        {
            return _context.Dealers.AsQueryable();
        }
    }
}
