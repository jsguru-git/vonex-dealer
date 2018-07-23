using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class isdnchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndRange",
                table: "ISDNs");

            migrationBuilder.RenameColumn(
                name: "StartRange",
                table: "ISDNs",
                newName: "NumbersAsString");

            migrationBuilder.AddColumn<long>(
                name: "AddressID",
                table: "ISDNs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "HasRange",
                table: "ISDNs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RangeSize",
                table: "ISDNs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "ISDNs");

            migrationBuilder.DropColumn(
                name: "HasRange",
                table: "ISDNs");

            migrationBuilder.DropColumn(
                name: "RangeSize",
                table: "ISDNs");

            migrationBuilder.RenameColumn(
                name: "NumbersAsString",
                table: "ISDNs",
                newName: "StartRange");

            migrationBuilder.AddColumn<string>(
                name: "EndRange",
                table: "ISDNs",
                nullable: true);
        }
    }
}
