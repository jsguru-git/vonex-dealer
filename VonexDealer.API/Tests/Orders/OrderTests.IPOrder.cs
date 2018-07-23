using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Models.UB;
using Xunit;

namespace VonexDealer.API.Tests.Orders
{
    public partial class OrderTests
    {
        [Fact(DisplayName = "Order Add IPOrder")]
        public async void AddIPOrderToOrder()
        {
            HardwareOrder createHardwareOrder = new HardwareOrder() { Freight = 50.00, SubTotal = 150.55 };
            HardwareOrder hardwareOrder = await _order.CreateHardwareOrderAsync(createHardwareOrder);
            Assert.True(hardwareOrder.HardwareOrderID > 0);

            List<RatePlan> rateplans = _order.GetRatePlans().ToList();
            Assert.NotEmpty(rateplans);

            IPOrder createIPOrder = new IPOrder
            {
                callHandling = CallHandling.HuntGroup,
                CurrentModem = "Modem",
                CurrentSwitch = "switch",
                DownloadSpeed = "10 MBPS",
                UploadSpeed = "2 MBPS",
                RatePlanID = rateplans[0].RatePlanID,
            };
            IPOrder iporder = await _order.CreateIPOrderAsync(createIPOrder);

            Assert.True(iporder.IPOrderID > 0);
        }

        [Fact(DisplayName = "Order update IPOrder")]
        public async void UpdateIPOrderToOrder()
        {
            List<IPOrder> IPorders = _order.GetIPOrders(0).ToList();
            Assert.NotEmpty(IPorders);

            List<Order> orders = _order.GetOrders(0).ToList();
            Assert.NotEmpty(orders);

            Order order = await _cust.UpdateIPOrderToOrderAsync(orders[0].OrderID, IPorders[0].IPOrderID);
            Assert.Equal(orders[0].OrderID, order.OrderID);
            Assert.Equal(orders[0].IPOrderID, order.IPOrderID);
        }

        [Fact(DisplayName = "Order delete IPOrder")]
        public async void DeleteIPOrderToOrder()
        {
            List<Order> orders = _order.GetOrders(0).ToList();
            Assert.NotEmpty(orders);
            Order order = await _cust.RemoveIPOrderFromOrderAsync(orders[0].OrderID);
            Assert.NotNull(order);
            Assert.Equal(orders[0].OrderID, order.OrderID);

        }
    }
}
