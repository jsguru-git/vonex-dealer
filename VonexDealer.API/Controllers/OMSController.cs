using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VonexDealer.API.Models.UB;
using VonexDealer.API.Repository.OMS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VonexDealer.API.Controllers
{
    [Route("api/[controller]")]
    public class OMSController : Controller
    {
        private IOMS _oms;

        public OMSController(IOMS oms)
        {
            _oms = oms;
        }
        // GET: api/<controller>
        [HttpGet("GetAllPlans")]
        public async Task<IEnumerable<RatePlan>> GetAsync()
        {
            return await _oms.getPlansAsync();
        }

        // GET api/<controller>/5
        [HttpGet("GetPlansByMonth/{months}")]
        public async Task<IEnumerable<RatePlan>> GetAsync(int months)
        {
            return await _oms.getPlansAsync((int)months);
        }

        [HttpGet("GetPlansByCategoryAndMonth")]
        public async Task<IEnumerable<RatePlan>> GetAsync(string usageType, int months)
        {
            return await _oms.getPlansAsync(usageType, (int)months);
        }

        [HttpGet("GetUsageType")]
        public async Task<IEnumerable<string>> GetUsageType()
        {
            return await _oms.getUsageTypes();
        }

    }
}
