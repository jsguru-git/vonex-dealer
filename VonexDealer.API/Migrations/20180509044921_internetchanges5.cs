using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class internetchanges5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "ContactID",
                table: "Internet",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactEmail = table.Column<string>(nullable: true),
                    ContactMobile = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    OrderID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactID",
                table: "Internet");

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
    }
}
