using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Repository;
using VonexDealer.API.Repository.Orders;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace VonexDealer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public partial class OrderController : Controller
    {
        private ILogger<OrderController> _logger;
        private IOrderRepository _order;
        private ILandline _landline;
        private IConverter _pdfConverter;

        public OrderController(IOrderRepository order, ILogger<OrderController> logger, ILandline landline, IConverter pdfConverter)
        {
            _logger = logger;
            _order = order;
            _landline = landline;
            _pdfConverter = pdfConverter;
        }




    }
}
