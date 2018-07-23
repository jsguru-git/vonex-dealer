using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class orderchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DirectDebits_DirectDebitID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Hardware_HardwareID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_IPOrders_IPOrderID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Internet_InternetID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Landlines_LandlineDetailsLandlineID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DirectDebitID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_HardwareID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_IPOrderID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_InternetID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_LandlineDetailsLandlineID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LandlineDetailsLandlineID",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LandlineDetailsLandlineID",
                table: "Orders",
                nullable: true);

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
                name: "IX_Orders_LandlineDetailsLandlineID",
                table: "Orders",
                column: "LandlineDetailsLandlineID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DirectDebits_DirectDebitID",
                table: "Orders",
                column: "DirectDebitID",
                principalTable: "DirectDebits",
                principalColumn: "DirectDebitID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Hardware_HardwareID",
                table: "Orders",
                column: "HardwareID",
                principalTable: "Hardware",
                principalColumn: "HardwareID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_IPOrders_IPOrderID",
                table: "Orders",
                column: "IPOrderID",
                principalTable: "IPOrders",
                principalColumn: "IPOrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Internet_InternetID",
                table: "Orders",
                column: "InternetID",
                principalTable: "Internet",
                principalColumn: "InternetID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Landlines_LandlineDetailsLandlineID",
                table: "Orders",
                column: "LandlineDetailsLandlineID",
                principalTable: "Landlines",
                principalColumn: "LandlineID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
