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
        [Fact(DisplayName = "Landline NewService ")]
        public async Task LandlineTestAsync()
        {
            //add first
            Order orders = await _order.CreateOrderAsync(new Order { DealerID = 1, CustomerID = 1 });
            Assert.NotNull(orders);

            Landline landline = await _landline.AddLandlineAsync(new Landline { OrderID = orders.OrderID });
            Assert.True(landline.LandlineID > 0);

            NewPSTN pstn = new NewPSTN
            {
                Fee = 100,
                RatePlanID = 1,
                Termination = "MDF",
                LandlineID = landline.LandlineID
            };

            await _landline.AddNewPSTNAsync(pstn);
            Assert.True(pstn.NewPSTNID > 0);

            pstn.Termination = "Premises";
            await _landline.UpdateNewPSTNAsync(pstn.NewPSTNID, pstn);

            List<NewPSTN> pstns = await _landline.GetNewPSTNsOnOrder(landline.LandlineID);
            Assert.NotEmpty(pstns);

            Assert.True(pstn.Termination == pstns.FirstOrDefault().Termination);

            //Int64 landlineID = landline.LandlineID;
            //await _landline.RemoveLandlineAsync(landlineID);
            //await _landline.RemoveNewPSTNAsync(pstn.NewPSTNID);
            //var newlines = await _landline.GetNewPSTNsOnOrder(landlineID);
            //Assert.True(newlines.Count() == 0);

        }
    }
}
