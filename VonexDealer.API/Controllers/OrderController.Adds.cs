using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models;
using VonexDealer.API.Models.Order;
using VonexDealer.API.ViewModels;

namespace VonexDealer.API.Controllers
{
    public partial class OrderController : Controller
    {

        [AllowAnonymous]
        [HttpPost("AddSignature")]
        public async Task<IActionResult> AddSignature([FromBody] SignatureViewModel data)
        {
            Guid orderGuid = data.orderGUID;

            if (orderGuid == Guid.Empty)
                return BadRequest();
            bool result = await _order.UpdateSignature(orderGuid, data.data);
            if (result)
                return Ok(result);
            else
                return BadRequest();

        }
      

        [AllowAnonymous]
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }


        /// <summary>
        /// Add new Company to existing Order
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="company"></param>
        /// <returns>Order Object or null if error.</returns>
        [HttpPost("AddCompanyToOrder/{orderID}")]
        public async Task<IActionResult> AddCompanyToOrderAsync(Int64 orderID, [FromBody] Company company)
        {
            Company updatedOrder = await _order.AddCompanyToOrderAsync(orderID, company);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();
        }
        /// <summary>
        /// add new customer to order.
        /// This will also create customer on database.
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="customer"></param>
        /// <returns>Order Object or null if error.</returns>
        [HttpPost("AddCustomerToOrder/{orderID}")]
        public async Task<IActionResult> AddCustomerToOrderAsync(Int64 orderID, [FromBody] Customer customer)
        {

            Customer updatedOrder = await _order.AddCustomerToOrderAsync(orderID, customer);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }

        /// <summary>
        /// This endpoint will create an empty order - with dealerID.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null)
                return BadRequest();

            if (order.DealerID == 0)
                return BadRequest();

            Order updatedOrder = await _order.CreateOrderAsync(order);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }


        /// <summary>
        /// Create empty stub for order
        /// </summary>
        /// <param name="order"></param>
        /// <returns>order Objec or null if error.</returns>
        [HttpPost("AddOrder/{customerID}")]
        public async Task<IActionResult> AddOrderAsync(Int64 customerID, [FromBody] Order order)
        {
            if (order == null)
                return BadRequest();

            if (customerID == 0)
            {
                return BadRequest();
            }
            order.CustomerID = customerID;

            Order updatedOrder = await _order.CreateOrderAsync(order);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }

        /// <summary>
        /// Add Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>Company</returns>
        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody] Company company)
        {
            if (company == null)
                return BadRequest(company);

            Company newCompany = await _order.AddCompanyAsync(company);
            if (newCompany != null)
                return Ok(newCompany);
            else
            {
                return BadRequest(company);
            }
        }

        [HttpPost("AddInboundOrder/{orderID}")]
        public async Task<IActionResult> AddInboundOrderAsync(Int64 orderID, [FromBody] Inbound inbound)
        {

            Inbound updatedOrder = await _order.AddInboundToOrderAsync(orderID, inbound);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }
        [HttpPost("AddInternetOrder/{orderID}")]
        public async Task<IActionResult> AddInternetOrderAsync(Int64 orderID, [FromBody] List<Internet> internets)
        {

            List<Internet> updatedOrder = await _order.AddInternetToOrderAsync(orderID, internets);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return BadRequest();

        }
        [HttpPost("AddContact/{orderID}")]
        public async Task<IActionResult> AddContactAsync(Int64 orderID, [FromBody] List<Contact> contacts)
        {

            List<Contact> updateContact = await _order.AddContactsAsync(orderID, contacts);
            if (updateContact != null)
                return Ok(updateContact);
            else
                return BadRequest();

        }
        [HttpPost("AddIPOrder/{orderID}")]
        public async Task<IActionResult> AddIPOrderAsync(Int64 orderID, [FromBody] IPOrder iporder)
        {

            IPOrder updatedOrder = await _order.AddIPOrderToOrderAsync(orderID, iporder);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }


        [HttpPost("AddIPExtensionsToOrder/{orderID}")]
        public async Task<IActionResult> AddIPOrderAsync(Int64 orderID, [FromBody] IPExtension[] extensions)
        {

            List<IPExtension> iPExtensions = await _order.AddIPExtensionsToOrderAsync(orderID, extensions.ToList());
            if (iPExtensions != null)
                return Ok(iPExtensions);
            else
                return BadRequest();

        }



        [HttpPost("AddLandlineOrder/{orderID}")]
        public async Task<IActionResult> AddLandlineOrderAsync(Int64 orderID, [FromBody] Landline landline)
        {

            Landline updatedOrder = await _order.AddLandlineToOrderAsync(orderID, landline);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }

        [HttpPost("AddMobileOrder/{orderID}")]
        public async Task<IActionResult> AddMobileOrderAsync(Int64 orderID, [FromBody] Mobile mobile)
        {

            Mobile updatedOrder = await _order.AddMobileToOrderAsync(orderID, mobile);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }
        [HttpPost("AddNotesOrder/{orderID}")]
        public async Task<IActionResult> AddNotesOrderAsync(Int64 orderID, [FromBody] Note[] notes)
        {

            List<Note> updatedOrder = await _order.AddNotesToOrderAsync(orderID, notes.ToList());
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }
        [HttpPost("AddPAFOrder/{orderID}")]
        public async Task<IActionResult> AddPAFOrderAsync(Int64 orderID, [FromBody] PAF paf)
        {

            PAF updatedOrder = await _order.AddPAFToOrderAsync(orderID, paf);
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }


        [HttpPost("CreateHardwareOrder/{orderID}")]
        public async Task<IActionResult> CreateHardwareOrderAsync(Int64 orderID, [FromBody] HardwareOrder hardware)
        {
            if (orderID == 0)
                return BadRequest();

            Order order = await _order.GetOrders(orderID).FirstOrDefaultAsync();

            if (order == null)
                return BadRequest();
            hardware.HardwareOrderID = order.HardwareID;

            if (hardware.OrderID == 0)
            {
                hardware.OrderID = orderID;
            }
            HardwareOrder updatedOrder = new HardwareOrder();

            if (hardware.HardwareOrderID > 0)
                updatedOrder = await _order.UpdateHardwareOrderAsync(hardware);
            else
                updatedOrder = await _order.CreateHardwareOrderAsync(hardware);

            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }

        [HttpPost("AddHardwareToOrder/{orderID}")]
        public async Task<IActionResult> AddHardwareToOrderAsync(Int64 orderID, [FromBody] Hardware[] hardwares)
        {

            List<Hardware> updatedOrder = await _order.AddHardwareToOrderAsync(orderID, hardwares.ToList());
            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NoContent();

        }

    }
}
