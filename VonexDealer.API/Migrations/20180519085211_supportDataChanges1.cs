using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class supportDataChanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Handsets_HardwareCategories_HardwareCategoryID1",
                table: "Handsets");

            migrationBuilder.DropForeignKey(
                name: "FK_Handsets_Manufacturers_ManufacturerDetailsManufacturerID",
                table: "Handsets");

            migrationBuilder.DropIndex(
                name: "IX_Handsets_HardwareCategoryID1",
                table: "Handsets");

            migrationBuilder.DropIndex(
                name: "IX_Handsets_ManufacturerDetailsManufacturerID",
                table: "Handsets");

            migrationBuilder.DropColumn(
                name: "HardwareCategoryID1",
                table: "Handsets");

            migrationBuilder.DropColumn(
                name: "ManufacturerDetailsManufacturerID",
                table: "Handsets");

            migrationBuilder.AlterColumn<long>(
                name: "ManufacturerDetailsID",
                table: "Handsets",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "HardwareCategoryID",
                table: "Handsets",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ManufacturerDetailsID",
                table: "Handsets",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "HardwareCategoryID",
                table: "Handsets",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "HardwareCategoryID1",
                table: "Handsets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerDetailsManufacturerID",
                table: "Handsets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Handsets_HardwareCategoryID1",
                table: "Handsets",
                column: "HardwareCategoryID1");

            migrationBuilder.CreateIndex(
                name: "IX_Handsets_ManufacturerDetailsManufacturerID",
                table: "Handsets",
                column: "ManufacturerDetailsManufacturerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Handsets_HardwareCategories_HardwareCategoryID1",
                table: "Handsets",
                column: "HardwareCategoryID1",
                principalTable: "HardwareCategories",
                principalColumn: "HardwareCategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Handsets_Manufacturers_ManufacturerDetailsManufacturerID",
                table: "Handsets",
                column: "ManufacturerDetailsManufacturerID",
                principalTable: "Manufacturers",
                principalColumn: "ManufacturerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
