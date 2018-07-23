using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Repository.Supporting
{
    public partial class Supporting : ISupport
    {


        public async Task<bool> RemoveHandsetsAsync(Int64 handsetID)
        {
            if (handsetID == 0)
                return false;
            Handset handset = await _context.Handsets.Where(x => x.HandsetID == handsetID).FirstOrDefaultAsync();


            _context.Handsets.Remove(handset);
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



        public async Task<List<Handset>> UpdateHandsets(List<Handset> handsets)
        {
            if (handsets == null)
                return null;

            foreach (Handset handset in handsets)
            {
                if (handset.HandsetID == 0)
                    _context.Handsets.Add(handset);
                else
                    _context.Handsets.Update(handset);
            }

            try
            {
                await _context.SaveChangesAsync();
                return handsets;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }
        }


    }
}
