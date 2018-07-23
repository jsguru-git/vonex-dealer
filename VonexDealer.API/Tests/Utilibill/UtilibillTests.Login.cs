using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using wsUtbCustomer;
using Xunit;

namespace VonexDealer.API.Tests.Utilibill
{
    public partial class UtilibillTests
    {

        [Fact(DisplayName = "Utilibill Get Customer")]
        public void getUBCustomerAsync()
        {

            //. getCustomerResponse response = new getCustomerResponse();
            CustomerResponse response = _utilibill.GetCustomerAsync("504049").GetAwaiter().GetResult();

            Assert.NotNull(response.customer);
            //  _output.WriteLine(response.customer.ToString());
            Assert.True(response.customer.surname == "Smith");
        }

        [Fact(DisplayName = "Utilibill Add Customer")]
        public async Task addUBCustomerAsync()
        {
            Customer customer = new Customer
            {
                CompanyDetails =  new Company { ABN = "1234567890" },
                AddressDetails = new List<Address> { new Address(){ StreetAddress = "252 Sandy Creek Road",
                 PostCode = 4570,
                 State = "QLD",
                 Suburb = "Veteran"} },
                Email = "andrew.smith@alsconsulting.com.au",
                FirstName = "Andrew",
                Surname = "Smith",
                PhoneNumber = "0422482326",
                DealerID = 2034,
                Salutation = "Mr"

            };

            CustomerResponse response = await _utilibill.AddCustomerAsync(customer);

            Assert.NotNull(response.customer);
            //  _output.WriteLine(response.customer.ToString());
            Assert.True(Convert.ToInt32(response.customer.custNo) > 0);
        }

        [Fact(DisplayName = "Utilibill Edit Customer")]
        public void editUBCustomerAsync()
        {
            wsCustomer customer = new wsCustomer
            {
                custNo = "504050",
                abn = "1234567890",
                address = "252 Sandy Creek Road",
                email = "andrew.smith@alsconsulting.com.au",
                sal = "Mr",
                firstName = "Andrew",
                suburb = "Gympie",
                surname = "Smith",
                state = "QLD",
                phone = "0422482326",
                postcode = "4570",
                agent_id = "2034",
                bank_account_name = "Andrew Smith",
                bank_account_no = "54321",
                bank_bsb = "730130",
                bank_name = "westpac"


            };

            CustomerResponse response = _utilibill.EditCustomerAsync(customer).GetAwaiter().GetResult();

            Assert.NotNull(response.customer);
            //  _output.WriteLine(response.customer.ToString());
            Assert.True(Convert.ToInt32(response.customer.custNo) > 0);
        }

    }
}
