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
        [Fact(DisplayName = "Landline ISDN ")]
        public async Task ISDNTestAsync()
        {
            //add first
            Order orders = await _order.CreateOrderAsync(new Order { DealerID = 1, CustomerID = 1 });
            Assert.NotNull(orders);

            Landline landline = await _landline.AddLandlineAsync(new Landline { OrderID = orders.OrderID });
            Assert.True(landline.LandlineID > 0);


            ISDN isdn = new ISDN
            {
                ContractLengthMonths = 24,
                Numbers =  new List<string>() { "0754821230", "0754821330" },
                HasRange = true,
                AddressID = 1,
                RangeSize = 100,
                RatePlanID = 1,
                LandlineID = landline.LandlineID

            };

            await _landline.AddISDNAsync(isdn);
            Assert.True(isdn.ISDNID > 0);

            isdn.Numbers[0] = "0754821430";
            await _landline.UpdateISDNAsync(isdn.ISDNID, isdn);

            List<ISDN> isdns = await _landline.GetISDNsOnOrderAsync(landline.OrderID);

            Assert.NotEmpty(isdns);

            Assert.True(isdn.Numbers[0] == isdns.FirstOrDefault().Numbers[0]);

            //add to landline
            Order order = await _cust.UpdateLandLineToOrderAsync(orders.OrderID, landline.LandlineID);
            Assert.NotNull(order);


        }

        //[Theory(DisplayName = "landline delete ISDN")]
        //[InlineData(20)]
        //public async void DeleteISDN(Int64 landlineID)
        //{
        //    await _landline.RemoveLandlineAsync(landlineID);
        //    await _landline.RemoveISDNAsync(isdn.ISDNID);
        //    var churns = await _landline.GetISDNsOnOrderAsync(landlineID);
        //    Assert.True(churns.Count() == 0);

        //}
    }
}
