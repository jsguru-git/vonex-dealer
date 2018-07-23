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
        [Fact(DisplayName = "Order Add Customer")]
        public async void AddCustomerToOrder()
        {
            Order findOrder = _order.GetOrders(0).FirstOrDefault();
            Assert.NotNull(findOrder);

            Customer customer = new Customer
            {
                FirstName = "test",
                Surname = "test surname",
                DateAdded = DateTime.Now

            };
            Assert.NotNull(customer);

            Customer updatedOrder = await _order.AddCustomerToOrderAsync(findOrder.OrderID, customer);

            Assert.Equal(customer.CustomerID, updatedOrder.CustomerID);


        }

        [Fact(DisplayName = "Order update Customer")]
        public async void UpdateCustomerToOrder()
        {
            //need to check for the ability to update a single customer without deleting aspects
            //get first customer
            Customer cust = _cust.GetCustomers().FirstOrDefault();

            Customer newCust = cust;
            newCust.Surname = "another surname";
            //Assert.True(cust.FirstName != newCust.FirstName);
            //Assert.True(cust.Surname != newCust.Surname);
            //Assert.True(cust.CustNo != newCust.CustNo);
            Customer customer = await _cust.UpdateCustomerAsync(cust.CustomerID, newCust);
            //Assert.True(cust.FirstName == newCust.FirstName);
            Assert.True(customer.Surname == cust.Surname);
            //Assert.True(cust.CustNo == newCust.CustNo);
        }

        [Fact(DisplayName = "Order delete Customer")]
        public async void DeleteCustomerToOrder()
        {
            List<Order> Orders = _order.GetOrders(0).ToList();
            Assert.NotEmpty(Orders);

            Order order = await _cust.RemoveCustomerFromOrderAsync(Orders.Last().OrderID);
            Assert.NotNull(order);
            Assert.Equal(Orders.Last().OrderID, order.OrderID);

        }
    }
}
