using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class hardwareorders1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hardware_Addresses_AddressID",
                table: "Hardware");

            migrationBuilder.RenameColumn(
                name: "AddressID",
                table: "Hardware",
                newName: "HardwareOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_Hardware_AddressID",
                table: "Hardware",
                newName: "IX_Hardware_HardwareOrderID");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Hardware",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DealerSupplied",
                table: "Hardware",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "HandsetID",
                table: "Hardware",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Hardware",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HardwareOrders",
                columns: table => new
                {
                    HardwareOrderID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<long>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    OrderID = table.Column<long>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOrders", x => x.HardwareOrderID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Hardware_HardwareOrders_HardwareOrderID",
                table: "Hardware",
                column: "HardwareOrderID",
                principalTable: "HardwareOrders",
                principalColumn: "HardwareOrderID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hardware_HardwareOrders_HardwareOrderID",
                table: "Hardware");

            migrationBuilder.DropTable(
                name: "HardwareOrders");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Hardware");

            migrationBuilder.DropColumn(
                name: "DealerSupplied",
                table: "Hardware");

            migrationBuilder.DropColumn(
                name: "HandsetID",
                table: "Hardware");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Hardware");

            migrationBuilder.RenameColumn(
                name: "HardwareOrderID",
                table: "Hardware",
                newName: "AddressID");

            migrationBuilder.RenameIndex(
                name: "IX_Hardware_HardwareOrderID",
                table: "Hardware",
                newName: "IX_Hardware_AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hardware_Addresses_AddressID",
                table: "Hardware",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
