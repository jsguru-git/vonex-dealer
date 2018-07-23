using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VonexDealer.API.Models;
using VonexDealer.API.Repository;
using VonexDealer.API.Repository.OMS;

namespace VonexDealer.API.Controllers
{
   
    public class HomeController : Controller
    {
        private IOMS _oms;

        public HomeController(IOMS oms)
        {
            _oms = oms;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult GetPlans()
        {
            return Ok(_oms.getPlansAsync());

        }
    }
}
