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
        /// <summary>
        /// provide company details to remove company from customer on order.
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="companyID"></param>
        /// <returns>Order Object</returns>
        [HttpDelete("DeleteCompanyFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteCompanyFromOrderAsync(Int64 orderID, [FromBody] Int64 companyID)
        {
            bool order = await _order.RemoveCompanyFromOrderAsync(orderID, companyID);
            if (order)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpDelete("DeleteCustomerFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteCustomerFromOrder(Int64 orderID, [FromBody] Int64 customerID)
        {
            bool order = await _order.RemoveCompanyFromOrderAsync(orderID, customerID);
            if (order)
                return Ok(order);
            else
                return BadRequest();
        }


        [HttpDelete("DeleteCompany/{companyID}")]
        public async Task<IActionResult> DeleteCompanyAsync(Int64 companyID)
        {
            bool result = await _order.RemoveCompanyAsync(companyID);
            if (result)
                return Ok();
            else
                return BadRequest();
        }
        [HttpDelete("DeleteInboundFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteInboundFromOrder(Int64 orderID, [FromBody] Int64 inboundID)
        {
            bool order = await _order.RemoveCompanyFromOrderAsync(orderID, inboundID);
            if (order )
                return Ok(order);
            else
                return BadRequest();
        }
        [HttpDelete("DeleteInternetFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteInternetFromOrder(Int64 orderID, [FromBody] List<Internet> internets)
        {
            bool order = await _order.RemoveInternetFromOrderAsync(orderID, internets);
            if (order )
                return Ok(order);
            else
                return BadRequest();
        }
        [HttpDelete("DeleteIPFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteIPFromOrder(Int64 orderID, [FromBody] Int64 ipOrderID)
        {
            bool order = await _order.RemoveCompanyFromOrderAsync(orderID, ipOrderID);
            if (order)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpDelete("DeleteIPExtsFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteIPExtsFromOrder(Int64 orderID, [FromBody] IPExtension[] extensions)
        {
            IPExtension[] delExtensions = await _order.RemoveIPExtensionsAsync(orderID, extensions);
            if (delExtensions.Length > 0)
                return Ok(delExtensions);
            else
                return BadRequest();
        }

        [HttpDelete("DeletePSTNChurnFromOrder/{orderID}")]
        public async Task<IActionResult> DeletePSTNChurnFromOrder(Int64 orderID, [FromBody] Churn[] churns)
        {
            Churn[] delChurns = await _order.RemovePSTNChurnFromOrderAsync(orderID, churns);
            if (delChurns != null)
                return Ok(delChurns);
            else
                return BadRequest();
        }

        [HttpDelete("DeleteNewPSTNFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteNewPSTNFromOrder(Int64 orderID, [FromBody] NewPSTN[] pstns)
        {
            NewPSTN[] delPstns = await _order.RemoveNewPSTNFromOrderAsync(orderID, pstns);
            if (delPstns != null)
                return Ok(delPstns);
            else
                return BadRequest();
        }

        [HttpDelete("DeleteLandLineFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteLandLineFromOrder(Int64 orderID, [FromBody] Int64 landLineID)
        {
            bool order = await _order.RemoveCompanyFromOrderAsync(orderID, landLineID);
            if (order )
                return Ok(order);
            else
                return BadRequest();
        }
        [HttpDelete("DeleteMobileFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteMobileFromOrder(Int64 orderID, [FromBody] Int64 mobileID)
        {
            Boolean order = await _order.RemoveCompanyFromOrderAsync(orderID, mobileID);
            if (order)
                return Ok(order);
            else
                return BadRequest();
        }
        [HttpDelete("DeleteNotesFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteNotesFromOrder(Int64 orderID, [FromBody] Int64 notesID)
        {
            Boolean order = await _order.RemoveNotesFromOrderAsync(orderID, notesID);
            if (order )
                return Ok(order);
            else
                return BadRequest();
        }
        [HttpDelete("DeletePAFFromOrder/{orderID}")]
        public async Task<IActionResult> DeletePAFFromOrder(Int64 orderID, [FromBody] Int64 pafID)
        {
            bool order = await _order.RemoveCompanyFromOrderAsync(orderID, pafID);
            if (order )
                return Ok(order);
            else
                return BadRequest();
        }
        [HttpDelete("DeleteContactsFromOrder/{orderID}")]
        public async Task<IActionResult> DeleteContactsFromOrder(Int64 orderID, [FromBody] List<Contact> contacts)
        {
            bool order = await _order.RemoveContactsFromOrderAsync(orderID, contacts);
            if (order)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpDelete("RemoveHardwareOnOrderAsync/{orderID}")]
        public async Task<IActionResult> RemoveHardwareOnOrderAsync(Int64 orderID, [FromBody] List<Hardware> IPRows)
        {
            bool order = await _order.RemoveHardwareOnOrderAsync(orderID, IPRows);
            if (order)
                return Ok(order);
            else
                return BadRequest();
        }

    }
}
