using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VonexDealer.API.Models.Order;
using VonexDealer.API.ViewModels;
using DinkToPdf;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VonexDealer.API.Controllers
{

    public partial class OrderController : Controller
    {
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailAsync([FromBody] Message message)
        {
            Boolean result = await _order.SendEmailAsync(message, User);
            if (result)
            {
                await _order.UpdateOrderStatus(message.OrderID, (int)Status.InProgress);
                return Ok(result);
            }
            else
                return BadRequest(result);
        }

        [HttpPost("SendCompiledOrderEmail/{orderID}")]
        public async Task<IActionResult> SendCompiledOrderEmailAsync(Int64 orderID, [FromBody] Message message)
        {

            var view = View("orderReport/" + orderID);
            var html = view.ToHtml(this.HttpContext);

            Boolean result = await _order.SendEmailAsync(message, User);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }


        [HttpPost("OutputPDF")]
        public FileContentResult OutputPDF([FromBody] OrderReport orderReport)
        {
            if (orderReport.htmlContent == null)
                return null;
            var output = _pdfConverter.Convert(new HtmlToPdfDocument()
            {
                Objects = { new ObjectSettings() { HtmlContent = orderReport.htmlContent } }
            });


            //using (FileStream stream = new FileStream(@"Files\" + DateTime.UtcNow.Ticks.ToString() + ".pdf", FileMode.Create))
            //{
            //    stream.Write(output, 0, output.Length);
            //}

            return File(output, "application/pdf", orderReport.orderID + ".pdf");
        }


        [HttpGet("OrderReport/{orderID}")]
        public async Task<IActionResult> OrderReport(Int64 orderID = 0)
        {

            OrderView orderView = await _order.GetOrderReport(orderID);
            orderView.FrontEndURL = _order.GetApplicationUrl();
            if (orderView == null)
                return BadRequest();

            var result = View("OrderReport", orderView).ToHtml(this.HttpContext);
            OrderReport orderReport = new OrderReport
            {
                htmlContent = result,
                orderID = orderView.order.OrderID,
                order = orderView.order
            };
            return Ok(orderReport);
        }

        [AllowAnonymous]
        [HttpGet("OrderReportByGuid/{orderGUID}")]
        public async Task<IActionResult> OrderReportByGuid(Guid orderGUID)
        {
            if (orderGUID == Guid.Empty)
                return BadRequest();
            OrderView orderView = await _order.GetOrderReport(0, orderGUID);
            if (orderView == null)
                return BadRequest();
            var result = View("OrderReport", orderView).ToHtml(this.HttpContext);
            OrderReport orderReport = new OrderReport {
                htmlContent = result,
                orderID = orderView.order.OrderID,
                order = orderView.order
            };
            return Ok(orderReport);
        }

    }
}
