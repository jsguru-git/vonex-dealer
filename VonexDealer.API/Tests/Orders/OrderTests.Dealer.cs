using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using Xunit;

namespace VonexDealer.API.Tests.Orders
{
    public partial class OrderTests
    {
        [Fact(DisplayName = "Dealer Get")]
        public async void getDealer()
        {
            List<Dealer> dealers = _order.getDealers().ToList();

            Assert.NotEmpty(dealers);

            Dealer dealer = await _order.getDealerAsync(dealers[0].DealerID);

            Assert.NotNull(dealer);

        }
        [Fact(DisplayName = "Dealer Add")]
        public async void addDealer()
        {
            Dealer dealer = await _order.addDealer(new Dealer { DealerEmail = "sandeep@gmail.com", DealerFullName = "Sandeep Kumar", UBDealerID = 37 });
            Assert.True(dealer.DealerID > 0);
            Assert.True(dealer.DealerFullName == "Sandeep Kumar");
            Assert.True(dealer.DealerEmail == "sandeep@gmail.com");
        }
        [Fact(DisplayName = "Dealer Edit")]
        public async void editDealer()
        {
            List<Dealer> dealers = _order.getDealers().ToList();

            Assert.NotEmpty(dealers);
            var dealerId = dealers.Last().DealerID;
            Dealer dealer = await _order.updateDealer(dealerId, new Dealer { DealerEmail = "sandeepku@gmail.com", DealerFullName = "Sandeep Kumar", UBDealerID = 37, DealerID = dealerId });
            Assert.True(dealer.DealerID == dealerId);
        }
        [Fact(DisplayName = "Dealer Remove")]
        public async void removeDealer()
        {
            List<Dealer> dealers = _order.getDealers().ToList();

            Assert.NotEmpty(dealers);
            bool dealer = await _order.removeDealer(dealers.Last().DealerID);
            Assert.False(dealer );
        }
    }
}
