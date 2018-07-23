using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.IO;
using VonexDealer.API.Data;
using VonexDealer.API.Repository;

namespace VonexDealer.API.Tests.Utilibill
{
    public partial class UtilibillTests
    {
        private UtilibillRepository _utilibill;

        public UtilibillTests()//ITestOutputHelper output)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.Development.json", optional: false)
            .AddUserSecrets<Startup>()
            .Build();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"])
                    .Options;
            ApplicationDbContext context = new ApplicationDbContext(options);


            UtilibillRepository utilibillRepository = new UtilibillRepository(new Mock<ILogger<UtilibillRepository>>().Object, configuration, context);
            _utilibill = utilibillRepository;
        }


    }
}
