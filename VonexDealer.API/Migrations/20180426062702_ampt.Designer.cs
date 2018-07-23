﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using VonexDealer.API.Data;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180426062702_ampt")]
    partial class ampt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("VonexDealer.API.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Address", b =>
                {
                    b.Property<long>("AddressID")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CustomerID");

                    b.Property<int>("PostCode");

                    b.Property<string>("State");

                    b.Property<string>("StreetAddress");

                    b.Property<string>("Suburb");

                    b.HasKey("AddressID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Churn", b =>
                {
                    b.Property<long>("ChurnID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AddressID");

                    b.Property<string>("ContractPeriod");

                    b.Property<long>("LandlineID");

                    b.Property<long>("RatePlanID");

                    b.Property<string>("ServiceNumber");

                    b.HasKey("ChurnID");

                    b.HasIndex("LandlineID");

                    b.ToTable("Churns");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Company", b =>
                {
                    b.Property<long>("CompanyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ABN");

                    b.Property<string>("ACN");

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("DirectorGivenName");

                    b.Property<string>("DirectorSurname");

                    b.Property<string>("DriversLicenseNumber");

                    b.Property<string>("EntityType");

                    b.Property<string>("TradingName");

                    b.HasKey("CompanyID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Customer", b =>
                {
                    b.Property<long>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AddressID");

                    b.Property<long?>("BillRunCycleNumber");

                    b.Property<long?>("CompanyID");

                    b.Property<string>("ContactNo");

                    b.Property<string>("Contract_Term");

                    b.Property<string>("Contract_end");

                    b.Property<string>("Contract_start");

                    b.Property<string>("CustNo");

                    b.Property<DateTime>("DOB");

                    b.Property<DateTime?>("DateAdded");

                    b.Property<long?>("DealerID");

                    b.Property<string>("Email");

                    b.Property<string>("EntityType");

                    b.Property<string>("Fax");

                    b.Property<string>("FirstName");

                    b.Property<string>("GroupNo");

                    b.Property<string>("Mobile");

                    b.Property<string>("Phone");

                    b.Property<string>("PhoneNumber");

                    b.Property<long?>("PlanNo");

                    b.Property<string>("Position");

                    b.Property<string>("PrimaryContactName");

                    b.Property<string>("Salutation");

                    b.Property<string>("Surname");

                    b.Property<long>("UBAccountNumber");

                    b.HasKey("CustomerID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Dealer", b =>
                {
                    b.Property<long>("DealerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DealerEmail")
                        .IsRequired();

                    b.Property<string>("DealerFullName")
                        .IsRequired();

                    b.Property<long>("UBDealerID");

                    b.HasKey("DealerID");

                    b.ToTable("Dealers");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.DirectDebit", b =>
                {
                    b.Property<long>("DirectDebitID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName");

                    b.Property<string>("AccountNumber");

                    b.Property<string>("BSB");

                    b.Property<string>("FinancialInstitutionBranch");

                    b.Property<string>("FinancialInstitutionName");

                    b.HasKey("DirectDebitID");

                    b.ToTable("DirectDebits");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Handset", b =>
                {
                    b.Property<long>("HandsetID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("DealerPrice");

                    b.Property<string>("Description");

                    b.Property<long?>("HardwareCategoryID");

                    b.Property<int?>("HardwareCategoryID1");

                    b.Property<string>("ImagePath");

                    b.Property<long?>("ManufacturerDetailsID");

                    b.Property<int?>("ManufacturerDetailsManufacturerID");

                    b.Property<string>("Model");

                    b.Property<double>("RetailCost");

                    b.HasKey("HandsetID");

                    b.HasIndex("HardwareCategoryID1");

                    b.HasIndex("ManufacturerDetailsManufacturerID");

                    b.ToTable("Handsets");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Hardware", b =>
                {
                    b.Property<long>("HardwareID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AddressID");

                    b.HasKey("HardwareID");

                    b.HasIndex("AddressID");

                    b.ToTable("Hardware");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.HardwareCategory", b =>
                {
                    b.Property<int>("HardwareCategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.HasKey("HardwareCategoryID");

                    b.ToTable("HardwareCategories");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Internet", b =>
                {
                    b.Property<long>("InternetID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ConnectionFee");

                    b.Property<string>("ContractTerm");

                    b.Property<string>("InternetType");

                    b.Property<decimal>("MonthlyPrice");

                    b.Property<string>("PlanCode");

                    b.Property<string>("SpeedRequested");

                    b.Property<string>("UsageAllowance");

                    b.HasKey("InternetID");

                    b.ToTable("Internet");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.IPExtension", b =>
                {
                    b.Property<long>("IPExtensionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExtensionName");

                    b.Property<int>("ExtensionNo");

                    b.Property<long>("HandsetID");

                    b.Property<string>("MobileTwinning");

                    b.Property<long?>("OrderID");

                    b.Property<string>("OutboundANI");

                    b.Property<long>("RatePlanID");

                    b.Property<bool>("Voicemail");

                    b.Property<string>("VoicemailEmail");

                    b.Property<int>("contractLengthMonths");

                    b.HasKey("IPExtensionID");

                    b.ToTable("IPExtensions");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.IPOrder", b =>
                {
                    b.Property<long>("IPOrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AMPTDomainName");

                    b.Property<string>("CurrentModem");

                    b.Property<string>("CurrentSwitch");

                    b.Property<string>("DownloadSpeed");

                    b.Property<bool>("IsCreatedByAMPT");

                    b.Property<long>("OrderID");

                    b.Property<long?>("RatePlanID");

                    b.Property<string>("UploadSpeed");

                    b.Property<int>("callHandling");

                    b.HasKey("IPOrderID");

                    b.ToTable("IPOrders");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.ISDN", b =>
                {
                    b.Property<long>("ISDNID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AddressID");

                    b.Property<int>("ContractLengthMonths");

                    b.Property<bool>("HasRange");

                    b.Property<long>("LandlineID");

                    b.Property<string>("NumbersAsString");

                    b.Property<int>("RangeSize");

                    b.Property<long>("RatePlanID");

                    b.HasKey("ISDNID");

                    b.HasIndex("LandlineID");

                    b.ToTable("ISDNs");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Landline", b =>
                {
                    b.Property<long>("LandlineID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("OrderID");

                    b.HasKey("LandlineID");

                    b.ToTable("Landlines");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Manufacturer", b =>
                {
                    b.Property<int>("ManufacturerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ManufacturerID");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.NewPSTN", b =>
                {
                    b.Property<long>("NewPSTNID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AddressID");

                    b.Property<int>("ContractPeriod");

                    b.Property<double>("Fee");

                    b.Property<long>("LandlineID");

                    b.Property<long>("RatePlanID");

                    b.Property<string>("Termination");

                    b.HasKey("NewPSTNID");

                    b.HasIndex("LandlineID");

                    b.ToTable("NewPSTNs");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Order", b =>
                {
                    b.Property<long>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long?>("CustomerID");

                    b.Property<long>("DealerID");

                    b.Property<long?>("DirectDebitID");

                    b.Property<long?>("HardwareID");

                    b.Property<long?>("IPOrderID");

                    b.Property<long?>("InternetID");

                    b.Property<long?>("LandlineID");

                    b.Property<int>("OrderStatus");

                    b.Property<string>("Signature");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("DealerID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("VonexDealer.API.Models.UB.RatePlan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AdditionalServices");

                    b.Property<int?>("ContractMonths");

                    b.Property<int?>("DataAllowance");

                    b.Property<bool>("DealerSupplied");

                    b.Property<string>("GroupName");

                    b.Property<bool?>("Handset");

                    b.Property<bool?>("HasSpecialOverride");

                    b.Property<bool>("MixnMatch");

                    b.Property<bool?>("MonitorDataUsage");

                    b.Property<float?>("MonthlyProfitPercent");

                    b.Property<decimal?>("MonthlyRetail");

                    b.Property<bool?>("PayInitialComms");

                    b.Property<string>("RatePlanDescription");

                    b.Property<int?>("RatePlanID");

                    b.Property<string>("RatePlanPDF");

                    b.Property<bool>("Residential");

                    b.Property<string>("UsageType");

                    b.HasKey("ID");

                    b.ToTable("RatePlans");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("VonexDealer.API.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("VonexDealer.API.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VonexDealer.API.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("VonexDealer.API.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Address", b =>
                {
                    b.HasOne("VonexDealer.API.Models.Order.Customer")
                        .WithMany("AddressDetails")
                        .HasForeignKey("CustomerID");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Churn", b =>
                {
                    b.HasOne("VonexDealer.API.Models.Order.Landline")
                        .WithMany("Churns")
                        .HasForeignKey("LandlineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Customer", b =>
                {
                    b.HasOne("VonexDealer.API.Models.Order.Company", "CompanyDetails")
                        .WithMany()
                        .HasForeignKey("CompanyID");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Handset", b =>
                {
                    b.HasOne("VonexDealer.API.Models.Order.HardwareCategory", "hardwareCategory")
                        .WithMany()
                        .HasForeignKey("HardwareCategoryID1");

                    b.HasOne("VonexDealer.API.Models.Order.Manufacturer", "ManufacturerDetails")
                        .WithMany()
                        .HasForeignKey("ManufacturerDetailsManufacturerID");
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Hardware", b =>
                {
                    b.HasOne("VonexDealer.API.Models.Order.Address", "AddressDetails")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.ISDN", b =>
                {
                    b.HasOne("VonexDealer.API.Models.Order.Landline")
                        .WithMany("ISDNs")
                        .HasForeignKey("LandlineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.NewPSTN", b =>
                {
                    b.HasOne("VonexDealer.API.Models.Order.Landline")
                        .WithMany("NewServices")
                        .HasForeignKey("LandlineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VonexDealer.API.Models.Order.Order", b =>
                {
                    b.HasOne("VonexDealer.API.Models.Order.Customer", "CustomerDetails")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.HasOne("VonexDealer.API.Models.Order.Dealer", "DealerDetails")
                        .WithMany()
                        .HasForeignKey("DealerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
