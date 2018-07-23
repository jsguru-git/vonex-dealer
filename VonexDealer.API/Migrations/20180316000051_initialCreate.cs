using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostCode = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    Suburb = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ABN = table.Column<string>(nullable: true),
                    ACN = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DirectorGivenName = table.Column<string>(nullable: true),
                    DirectorSurname = table.Column<string>(nullable: true),
                    DriversLicenseNumber = table.Column<string>(nullable: true),
                    EntityType = table.Column<int>(nullable: false),
                    TradingName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    DealerID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DealerEmail = table.Column<string>(nullable: false),
                    DealerFullName = table.Column<string>(nullable: false),
                    UBDealerID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.DealerID);
                });

            migrationBuilder.CreateTable(
                name: "DirectDebits",
                columns: table => new
                {
                    DirectDebitID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    BSB = table.Column<string>(nullable: true),
                    FinancialInstitutionBranch = table.Column<string>(nullable: true),
                    FinancialInstitutionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectDebits", x => x.DirectDebitID);
                });

            migrationBuilder.CreateTable(
                name: "HardwareCategories",
                columns: table => new
                {
                    HardwareCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareCategories", x => x.HardwareCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "HardwareOrder",
                columns: table => new
                {
                    HardwareOrderID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Freight = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOrder", x => x.HardwareOrderID);
                });

            migrationBuilder.CreateTable(
                name: "Internet",
                columns: table => new
                {
                    InternetID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConnectionFee = table.Column<decimal>(nullable: false),
                    ContractTerm = table.Column<string>(nullable: true),
                    InternetType = table.Column<string>(nullable: true),
                    MonthlyPrice = table.Column<decimal>(nullable: false),
                    PlanCode = table.Column<string>(nullable: true),
                    SpeedRequested = table.Column<string>(nullable: true),
                    UsageAllowance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internet", x => x.InternetID);
                });

            migrationBuilder.CreateTable(
                name: "Landline",
                columns: table => new
                {
                    LandlineID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landline", x => x.LandlineID);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ManufacturerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ManufacturerID);
                });

            migrationBuilder.CreateTable(
                name: "RatePlans",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractMonths = table.Column<int>(nullable: true),
                    DataAllowance = table.Column<int>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    Handset = table.Column<bool>(nullable: true),
                    HasSpecialOverride = table.Column<bool>(nullable: true),
                    MonitorDataUsage = table.Column<bool>(nullable: true),
                    MonthlyProfitPercent = table.Column<float>(nullable: true),
                    MonthlyRetail = table.Column<decimal>(nullable: true),
                    PayInitialComms = table.Column<bool>(nullable: true),
                    RatePlanDescription = table.Column<string>(nullable: true),
                    RatePlanID = table.Column<int>(nullable: true),
                    RatePlanPDF = table.Column<string>(nullable: true),
                    UsageType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatePlans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Hardware",
                columns: table => new
                {
                    HardwareID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardware", x => x.HardwareID);
                    table.ForeignKey(
                        name: "FK_Hardware_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<long>(nullable: true),
                    BillRunCycleNumber = table.Column<long>(nullable: true),
                    CompanyID = table.Column<long>(nullable: true),
                    ContactNo = table.Column<string>(nullable: true),
                    Contract_Term = table.Column<string>(nullable: true),
                    Contract_end = table.Column<string>(nullable: true),
                    Contract_start = table.Column<string>(nullable: true),
                    CustNo = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    DealerID = table.Column<long>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EntityType = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    GroupNo = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PlanNo = table.Column<long>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    PrimaryContactName = table.Column<string>(nullable: true),
                    Salutation = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    UBAccountNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Handsets",
                columns: table => new
                {
                    HandsetID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DealerPrice = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HardwareCategoryID = table.Column<long>(nullable: true),
                    HardwareCategoryID1 = table.Column<int>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ManufacturerDetailsID = table.Column<long>(nullable: true),
                    ManufacturerDetailsManufacturerID = table.Column<int>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    RetailCost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handsets", x => x.HandsetID);
                    table.ForeignKey(
                        name: "FK_Handsets_HardwareCategories_HardwareCategoryID1",
                        column: x => x.HardwareCategoryID1,
                        principalTable: "HardwareCategories",
                        principalColumn: "HardwareCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Handsets_Manufacturers_ManufacturerDetailsManufacturerID",
                        column: x => x.ManufacturerDetailsManufacturerID,
                        principalTable: "Manufacturers",
                        principalColumn: "ManufacturerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IPOrders",
                columns: table => new
                {
                    IPOrderID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentModem = table.Column<string>(nullable: true),
                    CurrentSwitch = table.Column<string>(nullable: true),
                    DownloadSpeed = table.Column<string>(nullable: true),
                    HardwareOrderID = table.Column<long>(nullable: true),
                    RatePlanDetailsID = table.Column<int>(nullable: true),
                    RatePlanID = table.Column<long>(nullable: true),
                    UploadSpeed = table.Column<string>(nullable: true),
                    callHandling = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPOrders", x => x.IPOrderID);
                    table.ForeignKey(
                        name: "FK_IPOrders_HardwareOrder_HardwareOrderID",
                        column: x => x.HardwareOrderID,
                        principalTable: "HardwareOrder",
                        principalColumn: "HardwareOrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPOrders_RatePlans_RatePlanDetailsID",
                        column: x => x.RatePlanDetailsID,
                        principalTable: "RatePlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HardwareOrderDetail",
                columns: table => new
                {
                    HardwareOrderDetailID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HandsetID = table.Column<long>(nullable: true),
                    HardwareOrderID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOrderDetail", x => x.HardwareOrderDetailID);
                    table.ForeignKey(
                        name: "FK_HardwareOrderDetail_Handsets_HandsetID",
                        column: x => x.HandsetID,
                        principalTable: "Handsets",
                        principalColumn: "HandsetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareOrderDetail_HardwareOrder_HardwareOrderID",
                        column: x => x.HardwareOrderID,
                        principalTable: "HardwareOrder",
                        principalColumn: "HardwareOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IPExtensions",
                columns: table => new
                {
                    IPExtensionID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExtensionName = table.Column<string>(nullable: true),
                    ExtensionNo = table.Column<int>(nullable: false),
                    HandsetSelectedHandsetID = table.Column<long>(nullable: true),
                    IPOrderID = table.Column<long>(nullable: true),
                    MobileTwinning = table.Column<string>(nullable: true),
                    OutboundANI = table.Column<string>(nullable: true),
                    RatePlanMixnMatchID = table.Column<int>(nullable: true),
                    VoicemailEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPExtensions", x => x.IPExtensionID);
                    table.ForeignKey(
                        name: "FK_IPExtensions_Handsets_HandsetSelectedHandsetID",
                        column: x => x.HandsetSelectedHandsetID,
                        principalTable: "Handsets",
                        principalColumn: "HandsetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPExtensions_IPOrders_IPOrderID",
                        column: x => x.IPOrderID,
                        principalTable: "IPOrders",
                        principalColumn: "IPOrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPExtensions_RatePlans_RatePlanMixnMatchID",
                        column: x => x.RatePlanMixnMatchID,
                        principalTable: "RatePlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<long>(nullable: false),
                    DealerID = table.Column<long>(nullable: false),
                    DirectDebitID = table.Column<long>(nullable: true),
                    HardwareID = table.Column<long>(nullable: true),
                    IPOrderID = table.Column<long>(nullable: true),
                    InternetID = table.Column<long>(nullable: true),
                    LandlineID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Dealers_DealerID",
                        column: x => x.DealerID,
                        principalTable: "Dealers",
                        principalColumn: "DealerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_DirectDebits_DirectDebitID",
                        column: x => x.DirectDebitID,
                        principalTable: "DirectDebits",
                        principalColumn: "DirectDebitID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Hardware_HardwareID",
                        column: x => x.HardwareID,
                        principalTable: "Hardware",
                        principalColumn: "HardwareID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_IPOrders_IPOrderID",
                        column: x => x.IPOrderID,
                        principalTable: "IPOrders",
                        principalColumn: "IPOrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Internet_InternetID",
                        column: x => x.InternetID,
                        principalTable: "Internet",
                        principalColumn: "InternetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Landline_LandlineID",
                        column: x => x.LandlineID,
                        principalTable: "Landline",
                        principalColumn: "LandlineID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressID",
                table: "Customers",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyID",
                table: "Customers",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Handsets_HardwareCategoryID1",
                table: "Handsets",
                column: "HardwareCategoryID1");

            migrationBuilder.CreateIndex(
                name: "IX_Handsets_ManufacturerDetailsManufacturerID",
                table: "Handsets",
                column: "ManufacturerDetailsManufacturerID");

            migrationBuilder.CreateIndex(
                name: "IX_Hardware_AddressID",
                table: "Hardware",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareOrderDetail_HandsetID",
                table: "HardwareOrderDetail",
                column: "HandsetID");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareOrderDetail_HardwareOrderID",
                table: "HardwareOrderDetail",
                column: "HardwareOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_IPExtensions_HandsetSelectedHandsetID",
                table: "IPExtensions",
                column: "HandsetSelectedHandsetID");

            migrationBuilder.CreateIndex(
                name: "IX_IPExtensions_IPOrderID",
                table: "IPExtensions",
                column: "IPOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_IPExtensions_RatePlanMixnMatchID",
                table: "IPExtensions",
                column: "RatePlanMixnMatchID");

            migrationBuilder.CreateIndex(
                name: "IX_IPOrders_HardwareOrderID",
                table: "IPOrders",
                column: "HardwareOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_IPOrders_RatePlanDetailsID",
                table: "IPOrders",
                column: "RatePlanDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DealerID",
                table: "Orders",
                column: "DealerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DirectDebitID",
                table: "Orders",
                column: "DirectDebitID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HardwareID",
                table: "Orders",
                column: "HardwareID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IPOrderID",
                table: "Orders",
                column: "IPOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InternetID",
                table: "Orders",
                column: "InternetID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LandlineID",
                table: "Orders",
                column: "LandlineID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "HardwareOrderDetail");

            migrationBuilder.DropTable(
                name: "IPExtensions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Handsets");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Dealers");

            migrationBuilder.DropTable(
                name: "DirectDebits");

            migrationBuilder.DropTable(
                name: "Hardware");

            migrationBuilder.DropTable(
                name: "IPOrders");

            migrationBuilder.DropTable(
                name: "Internet");

            migrationBuilder.DropTable(
                name: "Landline");

            migrationBuilder.DropTable(
                name: "HardwareCategories");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "HardwareOrder");

            migrationBuilder.DropTable(
                name: "RatePlans");
        }
    }
}
