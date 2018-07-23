using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class adddressdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressDetailsAddressID",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AddressDetailsAddressID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressDetailsAddressID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerID",
                table: "Addresses",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerID",
                table: "Addresses",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerID",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerID",
                table: "Addresses");

            migrationBuilder.AddColumn<long>(
                name: "AddressDetailsAddressID",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AddressID",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressDetailsAddressID",
                table: "Customers",
                column: "AddressDetailsAddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressDetailsAddressID",
                table: "Customers",
                column: "AddressDetailsAddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
