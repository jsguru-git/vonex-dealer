using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Data;
using VonexDealer.API.Models.UB;
using VonexDealer.API.Repository.OMS;
using Xunit;
using Xunit.Abstractions;

namespace VonexDealer.API.Tests.OMS
{
    public class TestOMS
    {
        private OMSRepository _oms;
        private ITestOutputHelper _output;

        public TestOMS(ITestOutputHelper output)
        {
            var configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddUserSecrets<CustomerTest>()
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.Development.json", optional: false)
       .Build();

            //user the local db
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                      .Options;
            ApplicationDbContext context = new ApplicationDbContext(options);

            OMSRepository oms = new OMSRepository(context);

            _oms = oms;
            _output = output;
        }

      
        [Theory]
        [InlineData((int)ContractMonths.NoContract)]
        [InlineData((int)ContractMonths.Year)]
        [InlineData((int)ContractMonths.TwoYears)]
        public async Task Get12MonthContractPlans_returnsListAsync(int months)
        {
            _output.WriteLine($"Test: {months}");
            List<RatePlan> plans = await _oms.getPlansAsync(months);
            _output.WriteLine($"Rows: {plans.Count()}");
            Assert.IsType<List<RatePlan>>(plans);
            Assert.NotEmpty(plans);
        }

        [Fact]
        public async void GetPlansByCategory()
        {
            List<RatePlan> plans = await _oms.getPlansAsync("voip", 0);
            Assert.NotEmpty(plans);
        }

        [Fact]
        public async void GetusageTypes()
        {
            List<string> plans = await _oms.getUsageTypes();
            Assert.NotEmpty(plans);
        }
    }
}
