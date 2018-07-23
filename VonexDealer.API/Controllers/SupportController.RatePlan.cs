using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Controllers
{
    public partial class SupportController : Controller
    {

        [HttpDelete("DeleteRatePlan/{ratePlanID}")]
        public async Task<IActionResult> RemoveRatePlan(Int64 ratePlanID)
        {
            if (ratePlanID == 0)
                return BadRequest();

            var result = await _support.RemoveRatePlanAsync(ratePlanID);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("UpdateRatePlans")]
        public async Task<IActionResult> UpdateHandsets([FromBody] RatePlan[] ratePlans)
        {
            if (ratePlans == null || ratePlans.Length == 0)
                return BadRequest();


            var result = await _support.UpdateRatePlansAsync(ratePlans.ToList());
            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("GetRatePlans")]
        public async Task<IActionResult> getRatePlansAsync([FromBody] DataDisplayManager filter)
        {



            (List<RatePlan>, int) ratePlans = await _support.GetRatePlans(filter);

            if (ratePlans.Item1 != null)
                return Ok(new { data= ratePlans.Item1, count= ratePlans.Item2});
            else
                return NoContent();

        }
    }
}
