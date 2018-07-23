using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using VonexDealer.API.Data;
using VonexDealer.API.Models.TotalCheck;
using VonexDealer.API.Repository;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Linq;

namespace VonexDealer.API.Tests
{
    public partial class CustomerTest
    {
        private TCRepository _TCrepo;
        private CustomerRepository _customer;

        public CustomerTest()
        {
            var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddUserSecrets<CustomerTest>()
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.Development.json", optional: false)
          .Build();


            TCRepository aBNRepository = new TCRepository(configuration);
            _TCrepo = aBNRepository;

            //user the local db
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"])
                      .Options;
            ApplicationDbContext context = new ApplicationDbContext(options);

            UtilibillRepository utilibillRepository = new UtilibillRepository(new Mock<ILogger<UtilibillRepository>>().Object, configuration, context);

            CustomerRepository customerRepository = new CustomerRepository(context, new Mock<ILogger<CustomerRepository>>().Object, configuration, utilibillRepository);
            _customer = customerRepository;
        }
        [Theory(DisplayName = "ABN Lookup")]
        [InlineData("alsconsulting","4570", "28975185433")]
        public async Task GetQuery_FromTotalCheckAsync(string businessName, string postcode, string expectedABN)
        {

            AbnEnhanced.SearchParameters searchParameters = new AbnEnhanced.SearchParameters
            {
                BusinessName = businessName,
                Postcode = postcode,
                Autocomplete = "false",
                Rows = "10",
                ABNStatus = "Active"
            };

            var addressInfo = await _TCrepo.getABNsAsync(searchParameters);
            Assert.NotEmpty(addressInfo.Results.Where(x => x.Abn==expectedABN).ToList());
        }

        [Theory(DisplayName = "ABN Lookup By ABN")]
        [InlineData("28975185433")]
        [InlineData("28 975 185 433")]
        public async Task FromTotalCheckABNAsync(string abn)
        {
           
            var addressInfo = await _TCrepo.getABNByIDAsync(abn);
            Assert.Equal("OK",addressInfo.Status );
            Assert.NotEmpty(addressInfo.Results.ToList());
            
        }



        [Theory(DisplayName = "Address Check")]
        [InlineData("100 kings par", "OK")]
        public async Task TotalCheckAddressInvalidAsync(string address, string expectedStatus)
        {
            TCAddress.SearchParameters searchParameters = new TCAddress.SearchParameters
            {
                Country = "AU",
                FormattedAddress = WebUtility.UrlEncode(address)
            };

            var addressInfo = await _TCrepo.getAddressesAsync(searchParameters);
            Assert.Matches(expectedStatus, addressInfo.Status);
        }

    }
}
