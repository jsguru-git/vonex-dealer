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
        [Fact(DisplayName = "Order Add Landline")]
        public async void AddLandlineToOrder()
        {
            Assert.False(true);

        }

        [Fact(DisplayName = "Order update Landline")]
        public async void UpdateLandlineToOrder()
        {
            List<Order> orders = _order.GetOrders(0).ToList();
            Assert.NotEmpty(orders);
            Order order = await _cust.UpdateLandLineToOrderAsync(orders[0].OrderID, 0);
            Assert.NotNull(order);
            Assert.Equal(orders[0].OrderID, order.OrderID);
            Assert.Equal(orders[0].LandlineID, order.LandlineID);
        }

        [Fact(DisplayName = "Order delete Landline")]
        public async void DeleteLandlineToOrder()
        {
            List<Order> orders = _order.GetOrders(0).ToList();
            Assert.NotEmpty(orders);
            Order order = await _cust.RemoveLandLineFromOrderAsync(orders[0].OrderID);
            Assert.NotNull(order);
            Assert.Equal(orders[0].OrderID, order.OrderID);
        }

       
       

    }
}
