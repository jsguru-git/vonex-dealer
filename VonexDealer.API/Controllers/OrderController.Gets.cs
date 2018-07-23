using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Models.UB;
using VonexDealer.API.ViewModels;

namespace VonexDealer.API.Controllers
{
    public partial class OrderController : Controller
    {

        [HttpPost("GetRatePlans")]
        public async Task<IActionResult> getRatePlansAsync([FromBody] string[] filter)
        {

            var result = await _order.GetRatePlans()
                 //.Where(x => filter == null || x.UsageType == filter)
                 .Where(x => x.RatePlanDescription != null)
                 .OrderBy(x => x.RatePlanDescription).ToListAsync();

            List<RatePlan> ratePlans = new List<RatePlan>();

            if (filter != null && filter.Length > 0)
            {
                ratePlans = (from r in result
                             where filter.Contains(r.UsageType)
                             select r).ToList();
            }
            else
                ratePlans = result;

            if (ratePlans != null)
                return Ok(ratePlans);
            else
                return NoContent();

        }

        // GET: api/<controller>
        /// <summary>
        /// Central repository of all available hardware from Vonex.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="manufacturerID"></param>
        /// <returns>Handset object.</returns>
        [HttpGet("GetHandsets")]
        [AllowAnonymous]
        public async Task<List<Handset>> ListHandsets(Int64 categoryID = 0, Int64 manufacturerID = 0)
        {
            var handsetView =  await _order.getHandsetsAsync(categoryID, manufacturerID);
            return handsetView.Select(x => x.Handset).ToList();
        }

        /// <summary>
        /// Retrieve all existing orders
        /// Enter an order ID or, use 0 to retrieve all of them.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet("GetOrders/{orderID}")]
        public IActionResult getOrders(Int64 orderID)
        {
            string dealerEmail = "";


            dealerEmail = User.Claims.Where(x => x.Type == "name")
                    .Select(x => x.Value)
                    .FirstOrDefault();


            List<Order> order = _order.GetOrders(orderID, dealerEmail).ToList();

            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetCompany/{companyID}")]
        public async Task<IActionResult> getCompany(Int64 companyID)
        {
            Company company = await _order.GetCompanyAsync(companyID);

            if (company != null)
            {
                return Ok(company);
            }
            else
            {
                return BadRequest(companyID);
            }
        }

        [HttpGet("GetAddresses/{orderID}")]
        public async Task<IActionResult> getAddresses(Int64 orderID)
        {
            if (orderID == 0)
                return BadRequest();

            List<Address> addresses = await _order.GetAddressesOnOrderAsync(orderID);

            if (addresses != null)
            {
                
                return Ok(addresses);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetIPOrder/{orderID}")]
        public async Task<IActionResult> getIPOrder(Int64 orderID)
        {
            if (orderID == 0)
                return BadRequest();

            IPOrder order = await _order.GetIpOnOrderAsync(orderID);

            if (order != null)
            {

                return Ok(order);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Get Ip Extensions on order.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet("GetIPExtensions/{orderID}")]
        public async Task<IActionResult> GetIPExtensions(Int64 orderID)
        {
            if (orderID == 0)
                return BadRequest();

            List<IPExtension> iPExtensions = await _order.GetIPExtensionsOnOrderAsync(orderID);
            if (iPExtensions != null)
                return Ok( iPExtensions);
            return BadRequest();
        }

        [HttpGet("GetNotes/{orderID}/{component}")]
        public async Task<IActionResult> getNotesOnOrderAsync(Int64 orderID, string component)
        {
            if(orderID == 0 || component == "home")
            {
                return NoContent();
            }
            List<Note> notes = await _order.GetNotesOnOrderAsync(orderID, component);
            if (notes.Count > 0)
                return Ok(notes);
            else
                return NoContent();


        }

        [HttpGet("GetInternet/{orderID}")]
        public async Task<IActionResult> getInternetOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0 )
            {
                return NoContent();
            }
            List<Internet> internets = await _order.getInternetOnOrderAsync(orderID);

            if (internets == null || internets.Count > 0)
                return Ok(internets);
            else
                return NoContent();


        }

        [HttpGet("GetContacts/{orderID}")]
        public async Task<IActionResult> getContactsOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
            {
                return NoContent();
            }
            List<Contact> contacts = await _order.GetContactsAsync(orderID);

            if (contacts == null || contacts.Count > 0)
                return Ok(contacts);
            else
                return NoContent();


        }

        [HttpGet("GetHardwareOrder/{orderID}")]
        public async Task<IActionResult> getHardwareOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
            {
                return NoContent();
            }
            HardwareOrder order = await _order.GetHardwareOrderAsync(orderID);

            if (order != null )
                return Ok(order);
            else
                return NoContent();


        }

        [HttpGet("GetHardwareOnOrder/{orderID}")]
        public async Task<IActionResult> getHardwareOnOrderAsync(Int64 orderID)
        {
            if (orderID == 0)
            {
                return NoContent();
            }
            List<Hardware> hardwares = await _order.GetHardwareOnOrderAsync(orderID);

            if (hardwares == null || hardwares.Count > 0)
                return Ok(hardwares);
            else
                return NoContent();
        }

        [HttpGet("GetHandsetsOffOrder/{orderID}")]
        public async Task<IActionResult> getHandsetsOffOrder(Int64 orderID)
        {
            if (orderID == 0)
            {
                return NoContent();
            }
            List<Hardware> hardwares = await _order.getHandsetsOffOrder(orderID);

            if (hardwares == null || hardwares.Count > 0)
                return Ok(hardwares);
            else
                return NoContent();
        }

        [HttpGet("GetHardwareManufacturers")]
        public async Task<IActionResult> getHardwareManufacturers()
        {
            List<Manufacturer> manufacturers =  await _order.getManufacturersAsync();
            if (manufacturers != null)
            {
                return Ok(manufacturers);

            }
            else
                return NoContent();
        }

        [HttpGet("GetHardwareCategories")]
        public async Task<IActionResult> GetHardwareCategories()
        {
            List<HardwareCategory> categories = await _order.getHardwareCategoriesAsync();
            if (categories != null)
            {
                return Ok(categories);

            }
            else
                return NoContent();
        }
    }
}
