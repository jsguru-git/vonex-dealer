using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class rateplanchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdditionalServices",
                table: "RatePlans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DealerSupplied",
                table: "RatePlans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MixnMatch",
                table: "RatePlans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Residential",
                table: "RatePlans",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalServices",
                table: "RatePlans");

            migrationBuilder.DropColumn(
                name: "DealerSupplied",
                table: "RatePlans");

            migrationBuilder.DropColumn(
                name: "MixnMatch",
                table: "RatePlans");

            migrationBuilder.DropColumn(
                name: "Residential",
                table: "RatePlans");
        }
    }
}
