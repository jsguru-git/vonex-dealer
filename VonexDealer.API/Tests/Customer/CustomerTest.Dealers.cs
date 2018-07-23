using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using Xunit;

namespace VonexDealer.API.Tests
{
    public partial class CustomerTest
    {
        [Fact(DisplayName = "Dealers Get")]
        public async Task getDealersAsync()
        {
            List<Dealer> dealers = await _customer.getDealersAsync();
            Assert.NotEmpty(dealers);
        }

        [Fact(DisplayName = "Dealers Create")]
        public async Task CreateDealersAsync()
        {
            Dealer dealer = new Dealer
            {
                DealerEmail = "andrew@alsconsulting.com.au",
                DealerFullName = "Andrew Smith"

            };

            dealer = await _customer.CreateDealerAsync(dealer);

            Assert.True(dealer.DealerID > 0);
        }

        [Fact(DisplayName = "Dealer Update")]
        public async Task UpdateDealersAsync()
        {
            List<Dealer> dealers = await _customer.getDealersAsync();
            Dealer updatedDealer = dealers[dealers.Count-1];
            string fullName = updatedDealer.DealerFullName;

            updatedDealer.DealerFullName = "test";
            await _customer.UpdateDealerAsync(updatedDealer.DealerID, updatedDealer);

            var result = await _customer.getDealersAsync(updatedDealer.DealerID);

            Assert.True(updatedDealer.DealerFullName == result[0].DealerFullName);

            updatedDealer.DealerFullName = fullName;
            await _customer.UpdateDealerAsync(updatedDealer.DealerID, updatedDealer);
        }
        [Fact(DisplayName = "Dealers Delete")]
        public async Task DeleteDealersAsync()
        {

            List<Dealer> dealers =  await _customer.getDealersAsync();
            await _customer.DeleteDealerAsync(dealers[dealers.Count - 1].DealerID);
            List<Dealer> newDealers = await _customer.getDealersAsync();
            Assert.True(dealers.Count == newDealers.Count + 1);

            await _customer.CreateDealerAsync(dealers[dealers.Count - 1]);
            
        }
    }
}
