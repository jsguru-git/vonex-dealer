using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class landline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Landline_LandlineID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_LandlineID",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Landline",
                table: "Landline");

            migrationBuilder.RenameTable(
                name: "Landline",
                newName: "Landlines");

            migrationBuilder.AddColumn<long>(
                name: "LandlineDetailsLandlineID",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderID",
                table: "Landlines",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Landlines",
                table: "Landlines",
                column: "LandlineID");

            migrationBuilder.CreateTable(
                name: "Churns",
                columns: table => new
                {
                    ChurnID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<long>(nullable: false),
                    ContractPeriod = table.Column<string>(nullable: true),
                    LandlineID = table.Column<long>(nullable: false),
                    RatePlan = table.Column<string>(nullable: true),
                    ServiceNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Churns", x => x.ChurnID);
                    table.ForeignKey(
                        name: "FK_Churns_Landlines_LandlineID",
                        column: x => x.LandlineID,
                        principalTable: "Landlines",
                        principalColumn: "LandlineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ISDNs",
                columns: table => new
                {
                    ISDNID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractLengthMonths = table.Column<int>(nullable: false),
                    EndRange = table.Column<string>(nullable: true),
                    LandlineID = table.Column<long>(nullable: false),
                    RatePlanID = table.Column<long>(nullable: false),
                    StartRange = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISDNs", x => x.ISDNID);
                    table.ForeignKey(
                        name: "FK_ISDNs_Landlines_LandlineID",
                        column: x => x.LandlineID,
                        principalTable: "Landlines",
                        principalColumn: "LandlineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewPSTNs",
                columns: table => new
                {
                    NewPSTNID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<long>(nullable: false),
                    Fee = table.Column<double>(nullable: false),
                    LandlineID = table.Column<long>(nullable: true),
                    RatePlanID = table.Column<long>(nullable: false),
                    Termination = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewPSTNs", x => x.NewPSTNID);
                    table.ForeignKey(
                        name: "FK_NewPSTNs_Landlines_LandlineID",
                        column: x => x.LandlineID,
                        principalTable: "Landlines",
                        principalColumn: "LandlineID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LandlineDetailsLandlineID",
                table: "Orders",
                column: "LandlineDetailsLandlineID");

            migrationBuilder.CreateIndex(
                name: "IX_Churns_LandlineID",
                table: "Churns",
                column: "LandlineID");

            migrationBuilder.CreateIndex(
                name: "IX_ISDNs_LandlineID",
                table: "ISDNs",
                column: "LandlineID");

            migrationBuilder.CreateIndex(
                name: "IX_NewPSTNs_LandlineID",
                table: "NewPSTNs",
                column: "LandlineID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Landlines_LandlineDetailsLandlineID",
                table: "Orders",
                column: "LandlineDetailsLandlineID",
                principalTable: "Landlines",
                principalColumn: "LandlineID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Landlines_LandlineDetailsLandlineID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Churns");

            migrationBuilder.DropTable(
                name: "ISDNs");

            migrationBuilder.DropTable(
                name: "NewPSTNs");

            migrationBuilder.DropIndex(
                name: "IX_Orders_LandlineDetailsLandlineID",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Landlines",
                table: "Landlines");

            migrationBuilder.DropColumn(
                name: "LandlineDetailsLandlineID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Landlines");

            migrationBuilder.RenameTable(
                name: "Landlines",
                newName: "Landline");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Landline",
                table: "Landline",
                column: "LandlineID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LandlineID",
                table: "Orders",
                column: "LandlineID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Landline_LandlineID",
                table: "Orders",
                column: "LandlineID",
                principalTable: "Landline",
                principalColumn: "LandlineID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
