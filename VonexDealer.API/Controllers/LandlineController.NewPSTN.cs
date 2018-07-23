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
        /// Get NewPSTNs summary details for order.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet("GetNewPSTNsOnOrder/{orderID}")]
        public async Task<IActionResult> GetNewPSTNsOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
            {
                return BadRequest();
            }
            List<NewPSTN> lines = await _landline.GetNewPSTNsOnOrder(orderID);

            if (lines == null)
            {
                return NoContent();
            }

            return Ok(lines);

        }

        /// <summary>
        /// Add a single newPSTN to DB (not to order)
        /// </summary>
        /// <param name="newPSTN"></param>
        /// <returns></returns>
        [HttpPost("AddNewPSTNs/{landlineID}")]
        public async Task<IActionResult> AddNewPSTNs(Int64 landlineID, [FromBody] NewPSTN[] newPSTNs)
        {
            if (newPSTNs == null)
            {
                return BadRequest();
            }

            foreach (NewPSTN item in newPSTNs)
            {

                if (item == null)
                {
                    return BadRequest();
                }

                if (item.LandlineID != landlineID)
                {
                    item.LandlineID = landlineID;
                }

                if (await _landline.AddNewPSTNAsync(item) == null)
                {
                    return BadRequest();
                }


            }
            return Ok(newPSTNs);

        }

        /// <summary>
        /// Update newPSTN data
        /// </summary>
        /// <param name="newPSTNID"></param>
        /// <param name="newPSTN"></param>
        /// <returns></returns>
        [HttpPut("UpdateNewPSTN/{newPSTNID}")]
        public async Task<IActionResult> UpdateNewPSTN(Int64 newPSTNID, [FromBody] NewPSTN newPSTN)
        {
            if (newPSTNID == 0 || newPSTN == null)
            {
                return BadRequest();
            }
            NewPSTN line = await _landline.UpdateNewPSTNAsync(newPSTNID, newPSTN);

            if (line == null)
            {
                return BadRequest();
            }

            return Ok(line);

        }

        /// <summary>
        /// Remove newPSTN from DB
        /// </summary>
        /// <param name="newPSTNID"></param>
        /// <returns></returns>
        [HttpPost("RemoveNewPSTN/{newPSTNID}")]
        public async Task<IActionResult> RemoveNewPSTN(Int64 newPSTNID)
        {
            if (newPSTNID == 0)
            {
                return BadRequest();
            }
            bool line = await _landline.RemoveNewPSTNAsync(newPSTNID);

            if (!line)
            {
                return BadRequest();
            }

            return Ok(line);

        }
    }
}
