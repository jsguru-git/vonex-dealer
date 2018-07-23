using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Repository.Supporting;

namespace VonexDealer.API.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    public partial class SupportController : Controller
    {
        private ISupport _support;
        private ILogger<SupportController> _logger;

        public SupportController(ISupport support, ILogger<SupportController> logger)
        {
            _support = support;
            _logger = logger;

        }

        public IActionResult Index()
        {

            return View();
        }

    }
}
