using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VonexDealer.API.Models;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
           
        }
        public virtual DbSet<UBEnvironment> UBEnvironments { get; set; }
        public virtual DbSet<RatePlan> RatePlans { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Dealer> Dealers { get; set; }
        public virtual DbSet<Hardware> Hardware { get; set; }
        public virtual DbSet<HardwareOrder> HardwareOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<HardwareCategory> HardwareCategories { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<DirectDebit> DirectDebits { get; set; }
        public virtual DbSet<IPExtension> IPExtensions { get; set; }
        public virtual DbSet<IPOrder> IPOrders { get; set; }
        public virtual DbSet<Handset> Handsets { get; set; }
        public virtual DbSet<Internet> Internet { get; set; }
        public virtual DbSet<Landline> Landlines { get; set; }
        public virtual DbSet<Churn> Churns { get; set; }
        public virtual DbSet<ISDN> ISDNs { get; set; }
        public virtual DbSet<NewPSTN> NewPSTNs { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Auth> Auths { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

    }

}
