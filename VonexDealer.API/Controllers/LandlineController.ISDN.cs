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
        /// Get ISDNs summary details for order.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet("GetISDNsOnOrder/{orderID}")]
        public async Task<IActionResult> GetISDNsOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
            {
                return BadRequest();
            }
            List<ISDN> lines = await _landline.GetISDNsOnOrderAsync(orderID);

            if (lines == null)
            {
                return NoContent();
            }

            return Ok(lines);

        }

        /// <summary>
        /// Add isdn churn to order
        /// </summary>
        /// <param name="landlineID"></param>
        /// <param name="isdn"></param>
        /// <returns></returns>
        [HttpPost("AddISDNs/{landlineID}")]
        public async Task<IActionResult> AddISDNs(Int64 landlineID, [FromBody] ISDN[] isdns)
        {
            if (landlineID == 0 || isdns == null)
            {
                return BadRequest();
            }

            foreach (ISDN isdn in isdns)
            {

                isdn.LandlineID = landlineID;

                if (await _landline.AddISDNAsync(isdn) == null)
                {
                    return BadRequest();
                }
            }
            return Ok(isdns);

        }

        /// <summary>
        /// Update isdn data
        /// </summary>
        /// <param name="isdnID"></param>
        /// <param name="isdn"></param>
        /// <returns></returns>
        [HttpPut("UpdateISDN/{isdnID}")]
        public async Task<IActionResult> UpdateISDN(Int64 isdnID, [FromBody] ISDN isdn)
        {
            if (isdnID == 0 || isdn == null)
            {
                return BadRequest();
            }
            ISDN line = await _landline.UpdateISDNAsync(isdnID, isdn);

            if (line == null)
            {
                return BadRequest();
            }

            return Ok(line);

        }

        /// <summary>
        /// Remove isdn from DB
        /// </summary>
        /// <param name="isdnID"></param>
        /// <returns></returns>
        [HttpDelete("RemoveISDN/{isdnID}")]
        public async Task<IActionResult> RemoveISDN(Int64 isdnID)
        {
            if (isdnID == 0)
            {
                return BadRequest();
            }
            bool line = await _landline.RemoveISDNAsync(isdnID);

            if (!line)
            {
                return BadRequest();
            }

            return Ok(line);

        }
    }
}
