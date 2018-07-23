using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class rateplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatePlan",
                table: "Churns");

            migrationBuilder.AddColumn<long>(
                name: "RatePlanID",
                table: "Churns",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatePlanID",
                table: "Churns");

            migrationBuilder.AddColumn<string>(
                name: "RatePlan",
                table: "Churns",
                nullable: true);
        }
    }
}
