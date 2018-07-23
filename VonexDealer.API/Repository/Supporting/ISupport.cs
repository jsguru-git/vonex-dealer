using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Repository.Supporting
{
    public interface ISupport
    {
        /// <summary>
        /// Submit list of handsets to be created/updated.
        /// </summary>
        /// <param name="handsets"></param>
        /// <returns></returns>
        Task<List<Handset>> UpdateHandsets(List<Handset> handsets);
        Task<bool> RemoveHandsetsAsync(Int64 handsetID);

        /// <summary>
        /// Sublist list of RatePlans to be created/updated
        /// </summary>
        /// <param name="RatePlans"></param>
        /// <returns></returns>
        Task<List<RatePlan>> UpdateRatePlansAsync(List<RatePlan> RatePlans);
        Task<bool> RemoveRatePlanAsync(Int64 RatePlanID);
        Task<(List<RatePlan>, int count)> GetRatePlans(DataDisplayManager filter);


    }
}
