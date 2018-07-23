using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Repository.Supporting
{
    public partial class Supporting : ISupport
    {
        public async Task<bool> RemoveRatePlanAsync(Int64 RatePlanID)
        {
            if (RatePlanID == 0)
                return false;
            RatePlan RatePlan = await _context.RatePlans.Where(x => x.RatePlanID == RatePlanID).FirstOrDefaultAsync();


            _context.RatePlans.Remove(RatePlan);
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

        public async Task<List<RatePlan>> UpdateRatePlansAsync(List<RatePlan> RatePlans)
        {
            if (RatePlans == null)
                return null;

            foreach (RatePlan plan in RatePlans)
            {
                if (plan.RatePlanID == 0)
                    _context.RatePlans.Add(plan);
                else
                    _context.RatePlans.Update(plan);
            }

            try
            {
                await _context.SaveChangesAsync();
                return RatePlans;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }
        }

        public async Task<(List<RatePlan>, int count)> GetRatePlans(DataDisplayManager dm)
        {

            var DataSource = _context.RatePlans.AsQueryable();

            if (!string.IsNullOrEmpty(dm.SortBy))
            {
                string propName = dm.SortBy;
                propName = typeof(RatePlan).GetProperties().ToList().Where(x => x.Name.ToUpper() == dm.SortBy.ToUpper()).Select(x => x.Name).FirstOrDefault() ?? "";

                var propertyInfo = typeof(RatePlan).GetProperty(propName);
                if (dm.Descending)
                   DataSource =  DataSource.OrderByDescending(x => propertyInfo.GetValue(x, null));
                else
                    DataSource = DataSource.OrderBy(x => propertyInfo.GetValue(x, null));
            }

            //if (dm.Where != null && dm.Where.Count > 0) //Filtering
            //{
            //    result.result = operation.PerformWhereFilter(result.result, dm.Where, dm.Where[0].Operator);
            //}

            int totalCount = DataSource.Count();

            if (dm.Page != 0)
            {
                DataSource = DataSource.Skip((dm.Page - 1) * dm.RowsPerPage)
                    .Take(dm.RowsPerPage);
            }

            return (await DataSource.ToListAsync(), totalCount);

            // do your operations here 
        }

    }
}

