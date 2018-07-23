using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using Newtonsoft.Json;
using VonexDealer.API.Data;
using VonexDealer.API.Models.UB;
using VonexDealer.API.Models.Order;

namespace VonexDealer
{
    public static class DbContextExtension
    {

        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this ApplicationDbContext context)
        {

            if (!context.RatePlans.Any())
            {
                var types = JsonConvert.DeserializeObject<List<RatePlan>>(File.ReadAllText("Data\\seed" + Path.DirectorySeparatorChar + "rateplan.json"));
                context.AddRange(types);
                context.SaveChanges();
            }

            //if (!context.Customers.Any())
            //{
            //    var types = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText("Data\\seed" + Path.DirectorySeparatorChar + "customers.json"));
                
            //    context.AddRange(types);
            //    context.SaveChanges();

            //}

            //if (!context.Company.Any())
            //{
            //    var types = JsonConvert.DeserializeObject<List<Company>>(File.ReadAllText("Data\\seed" + Path.DirectorySeparatorChar + "company.json"));
            //    context.AddRange(types);
            //    context.SaveChanges();
            //}

            if (!context.Manufacturers.Any())
            {
                var types = JsonConvert.DeserializeObject<List<Manufacturer>>(File.ReadAllText("Data\\seed" + Path.DirectorySeparatorChar + "manufacturer.json"));
                context.AddRange(types);
                context.SaveChanges();
            }
            if (!context.HardwareCategories.Any())
            {
                var types = JsonConvert.DeserializeObject<List<HardwareCategory>>(File.ReadAllText("Data\\seed" + Path.DirectorySeparatorChar + "hardwarecategory.json"));
                context.AddRange(types);
                context.SaveChanges();
            }

            if (!context.Handsets.Any())
            {
                var types = JsonConvert.DeserializeObject<List<Handset>>(File.ReadAllText("Data\\seed" + Path.DirectorySeparatorChar + "handset.json"));
                context.AddRange(types);
                context.SaveChanges();
            }

            //if (!context.Dealers.Any())
            //{
            //    var types = JsonConvert.DeserializeObject<List<Dealer>>(File.ReadAllText("Data\\seed" + Path.DirectorySeparatorChar + "dealers.json"));
            //    context.AddRange(types);
            //    context.SaveChanges();
            //}

        }
    }

}