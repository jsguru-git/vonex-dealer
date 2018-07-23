using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Controllers
{
    public partial class LandlineController
    {
        /// <summary>
        /// Get Churns summary details for order.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet("GetChurnsOnOrder/{orderID}")]
        public async Task<IActionResult> GetChurnsOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
            {
                return BadRequest();
            }
            List<Churn> lines = await _landline.GetChurnsOnOrder(orderID);

            if (lines == null)
            {
                return BadRequest();
            }

            return Ok(lines);

        }

        /// <summary>
        /// Add churn to an existing landline order.
        /// </summary>
        /// <param name="landlineID"></param>
        /// <param name="churns"></param>
        /// <returns></returns>
        [HttpPost("AddChurns/{landlineID}")]
        public async Task<IActionResult> AddChurns(Int64 landlineID, [FromBody] Churn[] churns)
        {
            if (landlineID == 0)
            {
                return BadRequest();
            }

            foreach (Churn churn in churns)
            {


                if (churn == null)
                {
                    return BadRequest();
                }
                if (churn.LandlineID != landlineID)
                {
                    churn.LandlineID = landlineID;
                }
                //Churn line = 
                if(await _landline.AddChurnAsync(churn) == null)

                    return BadRequest();
                }

            
            return Ok(churns);

        }

        /// <summary>
        /// Update churn data
        /// </summary>
        /// <param name="churnID"></param>
        /// <param name="churn"></param>
        /// <returns></returns>
        [HttpPut("UpdateChurn/{churnID}")]
        public async Task<IActionResult> UpdateChurn(Int64 churnID, [FromBody] Churn churn)
        {
            if (churnID == 0 || churn == null)
            {
                return BadRequest();
            }
            Churn line = await _landline.UpdateChurnAsync(churnID, churn);

            if (line == null)
            {
                return BadRequest();
            }

            return Ok(line);

        }

        /// <summary>
        /// Remove churn from DB
        /// </summary>
        /// <param name="churnID"></param>
        /// <returns></returns>
        [HttpDelete("RemoveChurn/{churnID}")]
        public async Task<IActionResult> RemoveChurn(Int64 churnID)
        {
            if (churnID == 0)
            {
                return BadRequest();
            }
            bool line = await _landline.RemoveChurnAsync(churnID);

            if (!line)
            {
                return BadRequest();
            }

            return Ok(line);

        }

    }
}
