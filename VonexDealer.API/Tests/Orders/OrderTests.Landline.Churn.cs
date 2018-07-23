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
        // [Fact(DisplayName = "Landline Get Churns")]
        //[Fact(DisplayName = "Landline Create Churn")]
        //[Fact(DisplayName = "Landline Update Churn")]
        //[Fact(DisplayName = "Landline Delete Churn")]

      
        [Fact(DisplayName = "Landline Churn ")]
        public async Task ChurnTestAsync()
        {
            //add first
            Order orders = await _order.CreateOrderAsync(new Order { DealerID = 1, CustomerID = 1 });
            Assert.NotNull(orders);

            Landline landline = await _landline.AddLandlineAsync(new Landline { OrderID = orders.OrderID });
            Assert.True(landline.LandlineID > 0);

            Churn churn = new Churn { ContractPeriod = "24", RatePlanID = 100, ServiceNumber = "123456789", LandlineID = landline.LandlineID };

            await _landline.AddChurnAsync(churn);
            Assert.True(churn.ChurnID > 0);

            churn.RatePlanID = 100;
            await _landline.UpdateChurnAsync(churn.ChurnID, churn);

            List<Churn> churnB = await _landline.GetChurnsOnOrder(landline.LandlineID);
            Assert.NotEmpty(churnB);

            Assert.True(churn.RatePlanID == churnB.FirstOrDefault().RatePlanID);

            //Int64 landlineID = landline.LandlineID;
            //await _landline.RemoveLandlineAsync(landlineID);
            //await _landline.RemoveChurnAsync(churn.ChurnID);
            //var churns = await _landline.GetChurnsOnOrder(landlineID);
            //Assert.True(churns.Count() == 0);

        }

    }
}
