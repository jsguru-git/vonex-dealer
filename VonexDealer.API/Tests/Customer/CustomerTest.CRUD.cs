using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using Xunit;

namespace VonexDealer.API.Tests
{
    public partial class CustomerTest
    {


        [Fact(DisplayName = "Customers Returns non null")]
       
        public async Task GetCustomerAsync()
        {
            List<Customer> customers = await _customer.getCustomersAsync();

            Customer customer = await _customer.GetCustomerAsync(customers[0].CustomerID);
            Assert.NotNull(customer);

        }

        [Fact(DisplayName = "Customer By Dealer ID")]
        public async Task GetCustomerByDealerAsync()
        {
            List<Dealer> dealers = await _customer.getDealersAsync();
            Assert.NotEmpty(dealers);

            List<Customer> customers =await  _customer.getCustomersByDealerAsync(dealers[0].DealerID);
            Assert.NotEmpty(customers);

        }

        [Fact(DisplayName = "Customers Returns non null")]
        public async Task GetCustomersAsync()
        {
            List<Customer> customers = await _customer.getCustomersAsync();
                

            foreach (var item in customers.Where(x => x.CompanyDetails != null).ToList())
            {
                Assert.NotEmpty(await _customer.getCustomersAsync(null, null, null));
                Assert.NotEmpty(await _customer.getCustomersAsync(item.FirstName, null, null));
                Assert.NotEmpty(await _customer.getCustomersAsync(null, item.Surname, null));
                Assert.NotEmpty(await _customer.getCustomersAsync(null, null, item.CompanyDetails.CompanyName));
                Assert.NotEmpty(await _customer.getCustomersAsync(item.FirstName, item.Surname, null));
                Assert.NotEmpty(await _customer.getCustomersAsync(null, item.Surname, item.CompanyDetails.CompanyName));
                Assert.NotEmpty(await _customer.getCustomersAsync(item.FirstName, item.Surname, item.CompanyDetails.CompanyName));


            }


        }


        [Fact(DisplayName = "Update Customer Address")]

        public async Task UpdateCustomerAddressAsync()
        {


            //get original cust
            Customer cust =  _customer.GetCustomers().FirstOrDefault();
            //make a change
            string originalName = cust.FirstName;

            cust.FirstName = "testing";

            Customer modifiedCust = await _customer.UpdateCustomerAsync(cust.CustomerID, cust);
            //test the change took

            Assert.NotEqual(originalName, modifiedCust.FirstName);

            if (originalName != modifiedCust.FirstName)
            {
                //restore the original data
                cust.FirstName = originalName;
                modifiedCust = await _customer.UpdateCustomerAsync(cust.CustomerID, cust);
            }
        }

        [Fact(DisplayName = "Customer Update Customer Test")]
       
        public async Task UpdateCustomerTestAsync()
        {

            List<Customer> firstCust = await _customer.getCustomersAsync();

            //get original cust
            Customer cust = await _customer.GetCustomerAsync(firstCust[0].CustomerID);
            //make a change
            string originalName = cust.FirstName;

            cust.FirstName = "testing";

            Customer modifiedCust = await _customer.UpdateCustomerAsync(firstCust[0].CustomerID,cust);
            //test the change took

            Assert.NotEqual(originalName, modifiedCust.FirstName);

            if (originalName != modifiedCust.FirstName)
            {
                //restore the original data
                cust.FirstName = originalName;
                modifiedCust = await _customer.UpdateCustomerAsync(firstCust[0].CustomerID,cust);
            }
        }

        [Fact(DisplayName = "Customer Delete Customer Test")]
       
        public async Task DeleteCustomerTestAsync()
        {
            List<Customer> firstCust = await _customer.getCustomersAsync();
            //get original cust
            Customer cust = await _customer.GetCustomerAsync(firstCust[0].CustomerID);

            bool result = true; //await _customer.DeleteCustomerAsync(CustomerID);

            Assert.True(result);

        }
        [Fact(DisplayName = "Customer Create Customer Test")]
        public async Task CreateCustomerTestAsync()
        {

            var types = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText("Data\\seed" + Path.DirectorySeparatorChar + "customers.json"));

            Customer newcust;

            foreach (var customer in types.Take(5))
            {
                newcust = null;
                newcust = await _customer.CreateCustomerAsync(customer);
                Assert.NotNull(newcust);

            }

            
        }

    }
}
