using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Repository.OMS
{
    public interface IOMS
    {
        Task<List<RatePlan>> getPlansAsync();
        Task<List<RatePlan>> getPlansAsync(int contractMonths);
        Task<List<RatePlan>> getPlansAsync(string usageType, int contractMonths);
        Task<List<string>> getUsageTypes();
    }

    public enum ContractMonths
    {
        NoContract = 0,
        Year=12,
        TwoYears = 24
    }
}
