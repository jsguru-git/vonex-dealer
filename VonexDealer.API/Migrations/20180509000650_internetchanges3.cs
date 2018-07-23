using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class internetchanges3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Internet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactMobile",
                table: "Internet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Internet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Internet");

            migrationBuilder.DropColumn(
                name: "ContactMobile",
                table: "Internet");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Internet");
        }
    }
}
