using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class customerlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerID",
                table: "Addresses");

            migrationBuilder.AddColumn<long>(
                name: "AddressID",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerID",
                table: "Addresses",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerID",
                table: "Addresses",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");
        }
    }
}
