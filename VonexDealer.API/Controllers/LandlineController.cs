using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Repository;

namespace VonexDealer.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Vonex")]
    [Authorize]
    public partial class LandlineController : Controller
    {
        private ILogger<LandlineController> _logger;
        private ILandline _landline;

        public LandlineController(ILandline landline, ILogger<LandlineController> logger)
        {
            _logger = logger;
            _landline = landline;
        }
        /// <summary>
        /// Get Landline summary details for order.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet("GetLandlinesOnOrder/{orderID}")]
        public async Task<IActionResult> GetLandlinesOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
            {
                return BadRequest();
            }
            Landline lines = await _landline.GetLandlinesOnOrder(orderID);
            if (lines != null)
                return Ok(lines);
            else
                return NoContent();

        }

        /// <summary>
        /// Add a single landline to DB (not to order)
        /// </summary>
        /// <param name="landline"></param>
        /// <returns></returns>
        [HttpPost("AddLandlines")]
        public async Task<IActionResult> AddLandLines([FromBody] Landline landline)
        {
            if (landline == null || landline.LandlineID > 0)
            {
                return BadRequest();
            }
            Landline line = await _landline.AddLandlineAsync(landline);

            if (line == null)
            {
                return BadRequest();
            }

            return Ok(line);

        }

        /// <summary>
        /// Update landline data
        /// </summary>
        /// <param name="landlineID"></param>
        /// <param name="landline"></param>
        /// <returns></returns>
        [HttpPut("UpdateLandline/{landlineID}")]
        public async Task<IActionResult> UpdateLandline(Int64 landlineID, [FromBody] Landline landline)
        {
            if (landlineID == 0 || landline == null)
            {
                return BadRequest();
            }
            Landline line = await _landline.UpdateLandlineAsync(landlineID, landline);

            if (line == null)
            {
                return BadRequest();
            }

            return Ok(line);

        }

        /// <summary>
        /// Remove landline from DB
        /// </summary>
        /// <param name="landlineID"></param>
        /// <returns></returns>
        [HttpPost("RemoveLandline/{landlineID}")]
        public async Task<IActionResult> RemoveLandline(Int64 landlineID)
        {
            if (landlineID == 0)
            {
                return BadRequest();
            }
            bool line = await _landline.RemoveLandlineAsync(landlineID);

            if (!line)
            {
                return BadRequest();
            }

            return Ok(line);

        }

    }
}
