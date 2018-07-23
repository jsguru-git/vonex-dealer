using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Controllers
{
    public partial class OrderController : Controller
    {

        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany([FromBody] Company comp)
        {
            Company company = await _order.UpdateCompanyAsync(comp);
            if (company != null)
                return Ok(company);
            else
                return BadRequest();
        }


        [HttpPut("UpdateCompanyOnOrder/{orderID}")]
        public async Task<IActionResult> UpdateCompanyOnOrder(Int64 orderID, [FromBody] Company company)
        {
            Company order = await _order.UpdateCompanyOnOrderAsync(orderID, company);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpPut("UpdateCustomerOnOrder/{orderID}")]
        public async Task<IActionResult> UpdateCustomerOnOrder(Int64 orderID, [FromBody] Customer customer)
        {
            Customer order = await _order.UpdateCustomerOnOrderAsync(orderID, customer);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpPut("UpdateOrder/{orderID}")]
        public async Task<IActionResult> UpdateOrderAsync(Int64 orderID, [FromBody] Order updateOrder)
        {
            Order order = await _order.UpdateOrderAsync(orderID, updateOrder);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpPut("UpdateInboundOrder/{orderID}")]
        public async Task<IActionResult> UpdateInboundOrder(Int64 orderID, [FromBody] Inbound inbound)
        {
            Inbound order = await _order.UpdateInboundOnOrderAsync(orderID, inbound);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpPut("UpdateInternetOrder/{orderID}")]
        public async Task<IActionResult> UpdateInternetOrder(Int64 orderID, [FromBody] Internet internet)
        {
            Internet order = await _order.UpdateInternetOnOrderAsync(orderID, internet);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpPut("UpdateIPOrder/{orderID}")]
        public async Task<IActionResult> UpdateIPOrder(Int64 orderID, [FromBody] IPOrder iPOrder)
        {
            IPOrder order = await _order.UpdateIPOrderOnOrderAsync(orderID, iPOrder);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpPut("UpdateLandlineOrder/{orderID}")]
        public async Task<IActionResult> UpdateLandlineOrder(Int64 orderID, [FromBody] Landline landline)
        {
            Landline order = await _order.UpdateLandlineOnOrderAsync(orderID, landline);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

      
        [HttpPut("UpdateMobileOrder/{orderID}")]
        public async Task<IActionResult> UpdateMobileOrder(Int64 orderID, [FromBody] Mobile mobile)
        {
            Mobile order = await _order.UpdateMobileOnOrderAsync(orderID, mobile);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }
        [HttpPut("UpdateNotesOrder/{orderID}")]
        public async Task<IActionResult> UpdateNotesOrder(Int64 orderID, [FromBody] Note notes)
        {
            Note order = await _order.UpdateNotesOnOrderAsync(orderID, notes);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }
        [HttpPut("UpdatePAFOrder/{orderID}")]
        public async Task<IActionResult> UpdatePAFOrder(Int64 orderID, [FromBody] PAF paf)
        {
            PAF order = await _order.UpdatePAFOnOrderAsync(orderID, paf);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpPut("UpdateHardwareOrder/{orderID}")]
        public async Task<IActionResult> UpdateHardwareOrder(Int64 orderID, [FromBody] HardwareOrder hardware)
        {
            HardwareOrder order = await _order.UpdateHardwareOrderAsync(hardware);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

    }
}
