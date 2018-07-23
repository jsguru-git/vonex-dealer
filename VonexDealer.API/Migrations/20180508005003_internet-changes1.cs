using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class internetchanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanCode",
                table: "Internet");

            migrationBuilder.AddColumn<long>(
                name: "AddressID",
                table: "Internet",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrderID",
                table: "Internet",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RatePlanID",
                table: "Internet",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Internet");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Internet");

            migrationBuilder.DropColumn(
                name: "RatePlanID",
                table: "Internet");

            migrationBuilder.AddColumn<string>(
                name: "PlanCode",
                table: "Internet",
                nullable: true);
        }
    }
}
