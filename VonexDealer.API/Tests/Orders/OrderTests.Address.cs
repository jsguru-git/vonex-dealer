using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using Xunit;

namespace VonexDealer.API.Tests.Orders
{
    public partial class OrderTests
    {
        [Fact(DisplayName ="Order Add Address To Customer")]
        public async Task AddAddressToCustomerAsync()
        {

            Address address = new Address
            {
                PostCode = 4570,
                StreetAddress = "1 test street",
                Suburb = "Gympie",
                State = "QLD"
            };

            Address cust = await _cust.AddAddressToCustomerAsync(1, address);


            Assert.True(cust.AddressID > 0);

        }

    }
}
