using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Controllers
{
    public partial class SupportController : Controller
    {
        [HttpDelete("DeleteHandsets/{handsetID}")]
        public async Task<IActionResult> RemoveHandsets(Int64 handsetID)
        {
            if (handsetID ==  0 )
                return BadRequest();

            var result = await _support.RemoveHandsetsAsync(handsetID);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("UpdateHandsets")]
        public async Task<IActionResult> UpdateHandsets([FromBody] Handset[] handsets)
        {
            if (handsets == null || handsets.Length==0)
                return BadRequest();
           

            var result = await _support.UpdateHandsets(handsets.ToList());
            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
