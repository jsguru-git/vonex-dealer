using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressID",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AddressID",
                table: "Customers");

            migrationBuilder.AddColumn<long>(
                name: "AddressDetailsAddressID",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerID",
                table: "Addresses",
                nullable: false,
                defaultValue: 0L);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CustomerID",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressID",
                table: "Customers",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressID",
                table: "Customers",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
