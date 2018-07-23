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
        [Fact(DisplayName = "Order Add Internet")]
        public async void AddInternetToOrder()
        {
            List<Order> orders = _order.GetOrders(0).ToList();
            Assert.NotEmpty(orders);
            Internet internet = new Internet()
            {
                ConnectionFee = 200,
                ContractTerm = "1 year",
                InternetType = "ADSL",
                MonthlyPrice = 500,
                RatePlanID = 800,
                SpeedRequested = "2 MBPS",
                UsageAllowance = "4 GB"
            };
            Order order = await _cust.AddInternetToOrderAsync(orders[0].OrderID, internet);
        }

        [Fact(DisplayName = "Order update Internet")]
        public async void UpdateInternetToOrder()
        {
            List<Order> orders = _order.GetOrders(0).ToList();
            Assert.NotEmpty(orders);
            List<Internet> internets = _cust.GetInternet(0).ToList();
            Assert.NotEmpty(internets);
            Order order = await _cust.UpdateInternetToOrderAsync(orders[0].OrderID, internets[0].InternetID);
            Assert.NotNull(order);
            Assert.Equal(orders[0].OrderID, order.OrderID);
            Assert.Equal(orders[0].InternetID, order.InternetID);

        }

        [Fact(DisplayName = "Order delete Internet")]
        public async void DeleteInternetToOrder()
        {
            List<Order> orders = _order.GetOrders(0).ToList();
            Assert.NotEmpty(orders);
            Order order = await _cust.RemoveInternetFromOrderAsync(orders[0].OrderID);
            Assert.NotNull(order);
            Assert.Equal(orders[0].OrderID, order.OrderID);
        }
    }
}
