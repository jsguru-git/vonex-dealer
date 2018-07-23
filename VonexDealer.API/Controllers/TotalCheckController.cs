using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.TotalCheck;
using VonexDealer.API.Repository;

namespace VonexDealer.API.Controllers
{
    [Route("api/[controller]")]
    public class TotalCheckController : Controller
    {
        private ITCRepository _tcRepo;

        public TotalCheckController(ITCRepository tcRepository)
        {
            _tcRepo = tcRepository;

        }
        /// <summary>
        /// Validate ABN by name
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        [HttpPost("validateABN")]
        public async Task<IActionResult> ValidateABN([FromBody]AbnEnhanced.SearchParameters searchParameters)
        {
            if (searchParameters == null)
                return BadRequest();

            AbnEnhanced abnEnhanced = await _tcRepo.getABNsAsync(searchParameters);
            if (abnEnhanced.Status == "OK")
                return Ok(abnEnhanced);
            else
                return BadRequest(abnEnhanced);
        }

        /// <summary>
        /// Validate a company details via entering ABN
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("validateABN/{id}")]
        public async Task<IActionResult> ValidateABNFromID(string id)
        {
            if (id == null)
                return BadRequest();

            AbnEnhancedID abnEnhanced = await _tcRepo.getABNByIDAsync(id);
            if (abnEnhanced.Status == "OK")
                return Ok(abnEnhanced);
            else
                return BadRequest(abnEnhanced);
        }

        /// <summary>
        /// Get the validated details of a customer address
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        [HttpPost("/api/validateAddress")]
        public async Task<IActionResult> ValidateAddress([FromBody]TCAddress.SearchParameters searchParameters)
        {
            if (searchParameters == null)
                return BadRequest();

            TCAddress addresses = await _tcRepo.getAddressesAsync(searchParameters);
            if (addresses.Status == "OK")
                return Ok(addresses);
            else
                return BadRequest(addresses);
        }
    }
}
