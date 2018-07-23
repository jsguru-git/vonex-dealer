using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class ampt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AMPTDomainName",
                table: "IPOrders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCreatedByAMPT",
                table: "IPOrders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AMPTDomainName",
                table: "IPOrders");

            migrationBuilder.DropColumn(
                name: "IsCreatedByAMPT",
                table: "IPOrders");
        }
    }
}
