using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class iporderremoverateplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPOrders_RatePlans_RatePlanDetailsID",
                table: "IPOrders");

            migrationBuilder.DropIndex(
                name: "IX_IPOrders_RatePlanDetailsID",
                table: "IPOrders");

            migrationBuilder.DropColumn(
                name: "RatePlanDetailsID",
                table: "IPOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatePlanDetailsID",
                table: "IPOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IPOrders_RatePlanDetailsID",
                table: "IPOrders",
                column: "RatePlanDetailsID");

            migrationBuilder.AddForeignKey(
                name: "FK_IPOrders_RatePlans_RatePlanDetailsID",
                table: "IPOrders",
                column: "RatePlanDetailsID",
                principalTable: "RatePlans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
