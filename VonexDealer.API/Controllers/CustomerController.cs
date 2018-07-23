using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Repository;

namespace VonexDealer.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Vonex")]
    [Authorize]
    public class CustomerController : Controller
    {
        private ICustomerRepository _customer;

        public CustomerController(ICustomerRepository customer)
        {
            _customer = customer;
        }

        /// <summary>
        /// Get Customer details
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        [HttpGet( "GetCustomer/{CustomerID}")]
        public async Task<IActionResult> GetCustomerAsync(Int64 CustomerID)
        {
            Customer customer = await _customer.GetCustomerAsync(CustomerID);
            return Ok(customer);
        }

        /// <summary>
        /// Get a single address record
        /// </summary>
        /// <param name="AddressID"></param>
        /// <returns></returns>
        [HttpGet("GetAddress/{AddressID}")]
        public async Task<IActionResult> GetAddressAsync(Int64 AddressID)
        {
            if (AddressID == 0)
                return BadRequest();

            Address address = await _customer.GetAddressAsync(AddressID);

            if (address != null)
                return Ok(address);
            else
                return BadRequest();


        }
        /// <summary>
        /// Get a all customer address on record
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [HttpGet("GetAllCustomerAddress/{customerID}")]
        public async Task<IActionResult> GetAllCustomerAddressAsync(Int64 customerID)
        {
            if (customerID == 0)
                return BadRequest();

            List<Address> addresses = await _customer.GetAllCustomerAddressesAsync(customerID);

            if (addresses != null)
                return Ok(addresses);
            else
                return BadRequest();


        }


        /// <summary>
        /// Get dealers Customers.
        /// </summary>
        /// <param name="dealerID"></param>
        /// <returns></returns>
        [HttpGet("GetDealerCustomers/{dealerID}")]
        public async Task<IActionResult> GetDealerCustomersAsync(Int64 dealerID)
        {
            //todo: change this to logged in dealer.

            List<Customer> customers = await _customer.getCustomersByDealerAsync( dealerID);
            return Ok(customers);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] Customer customer)
        {
            if(customer == null)
            {
                return BadRequest();
            }
            customer.DOB = customer.DOB.AddDays(1);
            Customer custReturned = await _customer.CreateCustomerAsync(customer);

            if (custReturned != null)
            {

                return Ok(custReturned);
            }
            else
                return BadRequest();

            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] Customer item)
        {
            if (item == null ||  id == 0)
            {
                return BadRequest();
            }
            item.DOB = item.DOB.AddDays(1);
            Customer cust = await _customer.UpdateCustomerAsync(id, item);
            if (cust == null)
            {
                return NotFound();
            }


            return Ok(cust);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var todo = await _customer.DeleteCustomerAsync(id);
            if (todo == false)
            {
                return NotFound();
            }

           
            return new NoContentResult();
        }
        /// <summary>
        /// Add address to existing customer.
        /// Or if custID == 0 then add a global address.
        /// </summary>
        /// <param name="custID"></param>
        /// <param name="address"></param>
        /// <returns>Customer Object</returns>
        [HttpPost("addAddresstoCustomer/{custID}")]
        public async Task<IActionResult> AddAddressToCustomerAsync(Int64 custID, [FromBody] Address address)
        {
            Address cust = await _customer.AddAddressToCustomerAsync(custID, address);

            if (cust != null)
            {
                return Ok(cust);
            }
            else
                return NotFound();
        }

        [HttpPut("UpdateAddresstoCustomer/{custID}")]
        public async Task<IActionResult> UpdateAddressToCustomerAsync(Int64 custID, [FromBody] Address address)
        {
            if (address == null)
                return BadRequest();
            Address cust = await _customer.UpdateAddressToCustomerAsync(custID, address);

            if (cust != null)
            {
                return Ok(cust);
            }
            else
                return NotFound();
        }

        [HttpPost("AddCustomerToUB")]
        public async Task<IActionResult> AddCustomerToUB([FromBody] Customer cust)
        {
            if (cust.UBAccountNo > 0)
                return Ok(cust);

            Customer customer = await _customer.addCustomerToUBAsync(cust);
            if (customer != null)
            {
                return Ok(cust);
            }
            else
                return BadRequest();
        }

        [HttpPost("addAddress")]
        public async Task<IActionResult> AddAddressAsync([FromBody] Address address)
        {
            Address cust = await _customer.AddAddress( address);

            if (cust != null)
            {
                return Ok(cust);
            }
            else
                return NotFound();
        }

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddressAsync( [FromBody] Address address)
        {
            if (address == null)
                return BadRequest();
            Address cust = await _customer.UpdateAddress( address);

            if (cust != null)
            {
                return Ok(cust);
            }
            else
                return NotFound();
        }

    }
}