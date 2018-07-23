using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Data;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Repository.OMS
{
    public class OMSRepository : IOMS
    {
        private ApplicationDbContext _context;

        public OMSRepository(ApplicationDbContext context)
        {
            _context = context;
        }

      

        public async Task<List<RatePlan>> getPlansAsync()
        {
            return await _context.RatePlans.ToListAsync();
        }

        public async Task<List<RatePlan>> getPlansAsync(int contractMonths)
        {
            return await _context.RatePlans.Where(x => x.ContractMonths == contractMonths).ToListAsync();
        }

        public async Task<List<RatePlan>> getPlansAsync(string usageType, int contractMonths)
        {
            return await _context.RatePlans
                .Where(x => x.ContractMonths == contractMonths)
                .Where(x => x.UsageType == usageType )
                .ToListAsync();
        }

        public async Task<List<string>> getUsageTypes()
        {
            return await _context.RatePlans
                .Select(x => x.UsageType)
                .Distinct()
                .ToListAsync();
                
        }
    }
}
