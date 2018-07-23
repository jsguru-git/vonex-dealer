using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class landlinechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewPSTNs_Landlines_LandlineID",
                table: "NewPSTNs");

            migrationBuilder.AlterColumn<long>(
                name: "LandlineID",
                table: "NewPSTNs",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NewPSTNs_Landlines_LandlineID",
                table: "NewPSTNs",
                column: "LandlineID",
                principalTable: "Landlines",
                principalColumn: "LandlineID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewPSTNs_Landlines_LandlineID",
                table: "NewPSTNs");

            migrationBuilder.AlterColumn<long>(
                name: "LandlineID",
                table: "NewPSTNs",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_NewPSTNs_Landlines_LandlineID",
                table: "NewPSTNs",
                column: "LandlineID",
                principalTable: "Landlines",
                principalColumn: "LandlineID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
