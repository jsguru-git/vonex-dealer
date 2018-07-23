using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VonexDealer.API.Data;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Repository;
using VonexDealer.API.Repository.Orders;
using Xunit;

namespace VonexDealer.API.Tests.Orders
{
    /// <summary>
    /// all the tests for IOrderRepository
    /// </summary>
    public partial class OrderTests
    {
        private OrderRepository _order;
        private CustomerRepository _cust;
        private LandlineRepository _landline;

        public OrderTests()
        {
            var configuration = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddUserSecrets<CustomerTest>()
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
         .AddJsonFile($"appsettings.Development.json", optional: false)
         .AddUserSecrets<Startup>()
         .Build();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"])
                     .Options;
            ApplicationDbContext context = new ApplicationDbContext(options);


            LandlineRepository landlineRepository = new LandlineRepository(context, new Mock<ILogger<LandlineRepository>>().Object);

            _landline = landlineRepository;

            OrderRepository orderRepository = new OrderRepository(context, new Mock<ILogger<OrderRepository>>().Object, new Mock<IHostingEnvironment>().Object ,configuration, landlineRepository );
            _order = orderRepository;
            UtilibillRepository utilibillRepository = new UtilibillRepository(new Mock<ILogger<UtilibillRepository>>().Object, configuration,context);

            CustomerRepository cust = new CustomerRepository(context, new Mock<ILogger<CustomerRepository>>().Object, configuration, utilibillRepository);
            _cust = cust;

            
        }
        [Fact(DisplayName = "Get Categories")]
        public async Task getCategoriesAsync()
        {
            List<HardwareCategory> categories = await _order.getHardwareCategoriesAsync();

            Assert.NotEmpty(categories);
        }
        [Fact(DisplayName = "Get Manufacturers")]
        public async Task getManufacturersAsync()
        {
            List<Manufacturer> manufacturers = await _order.getManufacturersAsync();

            Assert.NotEmpty(manufacturers);
        }
        [Fact(DisplayName = "Retreive Handsets by Category")]
        public async Task RetrieveListOfHandsetsByCategoryAsync()
        {


            Assert.NotEmpty(await _order.getHandsetsAsync());
            Assert.NotEmpty(await _order.getHandsetsAsync(1));
            Assert.NotEmpty(await _order.getHandsetsAsync(0, 1));
        }

        [Fact(DisplayName = "Add Orders")]
        public async Task AddOrdersAsync()
        {
            Order order = new Order
            {
                CreatedDate = DateTime.Now,
                CustomerID = 1,
                DealerID = 1
                


            };
            var Neworders = await _order.CreateOrderAsync(order);
            Assert.True(Neworders.OrderID > 0);

        }

        [Theory(DisplayName = "Get Orders")]
        [InlineData(0)]
        public void getOrders(Int64 orderID)
        {
            var orders = _order.GetOrders(orderID);

            Assert.NotNull(orders);
        }

    }
}
