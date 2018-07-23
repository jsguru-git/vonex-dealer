using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Controllers
{
    public partial class OrderController
    {
        /// <summary>
        /// get Dealer info by ID
        /// </summary>
        /// <param name="dealerID"></param>
        /// <returns></returns>
        [HttpGet("getDealer/{dealerID}")]
        public async Task<IActionResult> getDealerAsync(Int64 dealerID)
        {
            Dealer dealer = await _order.getDealerAsync(dealerID);

            if (dealer != null)
            {
                return Ok(dealer);
            }
            else
                return NoContent();
        }

        /// <summary>
        /// Get's the logged in dealer details.
        /// </summary>
        /// <returns></returns>
        [HttpPost("getDealer")]
        public async Task<IActionResult> getLoggedInDealerAsync()
        {
            string dealerEmail = "";


            dealerEmail = User.Claims.Where(x => x.Type == "name")
                    .Select(x => x.Value)
                    .FirstOrDefault();

            Dealer dealer = _order.getDealers().Where(x => x.DealerEmail == dealerEmail).FirstOrDefault();


            if (dealer != null)
            {
                return Ok(dealer);
            }
            else
                return NoContent();
        }
        [HttpGet("getDealerFromEmail")]
        public IActionResult getDealerFromEmail(string email)
        {
            Dealer dealer = _order.getDealers().Where(x => x.DealerEmail == email).FirstOrDefault();

            if (dealer != null)
            {
                return Ok(dealer);
            }
            else
                return NoContent();
        }

        [HttpGet("listDealers")]
        public IActionResult listDealers()
        {
            List<Dealer> dealers = _order.getDealers().ToList();

            if (dealers != null)
            {
                return Ok(dealers);
            }
            else
                return NoContent();
        }

        [HttpGet("updateDealer/{dealerID}")]

        public async Task<IActionResult> updateDealerAsync(Int64 dealerID, [FromBody] Dealer dealer)
        {

            Dealer updatedDealer = await _order.updateDealer(dealerID, dealer);

            if (updatedDealer == null)
            {
                return BadRequest();
            }

            return Ok(updatedDealer);

        }

        [HttpDelete("deleteDealer/{dealerID}")]

        public async Task<IActionResult> deleteDealerAsync(Int64 dealerID)
        {

            bool updatedDealer = await _order.removeDealer(dealerID);

            if (updatedDealer)
            {
                return Ok();
            }

            return BadRequest();

        }


    }
}
