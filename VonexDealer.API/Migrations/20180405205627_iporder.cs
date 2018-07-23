using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class iporder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPExtensions_Handsets_HandsetSelectedHandsetID",
                table: "IPExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_IPExtensions_RatePlans_RatePlanMixnMatchID",
                table: "IPExtensions");

            migrationBuilder.DropIndex(
                name: "IX_IPExtensions_HandsetSelectedHandsetID",
                table: "IPExtensions");

            migrationBuilder.DropIndex(
                name: "IX_IPExtensions_RatePlanMixnMatchID",
                table: "IPExtensions");

            migrationBuilder.DropColumn(
                name: "HandsetSelectedHandsetID",
                table: "IPExtensions");

            migrationBuilder.DropColumn(
                name: "RatePlanMixnMatchID",
                table: "IPExtensions");

            migrationBuilder.AddColumn<long>(
                name: "HandsetID",
                table: "IPExtensions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RatePlanID",
                table: "IPExtensions",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandsetID",
                table: "IPExtensions");

            migrationBuilder.DropColumn(
                name: "RatePlanID",
                table: "IPExtensions");

            migrationBuilder.AddColumn<long>(
                name: "HandsetSelectedHandsetID",
                table: "IPExtensions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatePlanMixnMatchID",
                table: "IPExtensions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IPExtensions_HandsetSelectedHandsetID",
                table: "IPExtensions",
                column: "HandsetSelectedHandsetID");

            migrationBuilder.CreateIndex(
                name: "IX_IPExtensions_RatePlanMixnMatchID",
                table: "IPExtensions",
                column: "RatePlanMixnMatchID");

            migrationBuilder.AddForeignKey(
                name: "FK_IPExtensions_Handsets_HandsetSelectedHandsetID",
                table: "IPExtensions",
                column: "HandsetSelectedHandsetID",
                principalTable: "Handsets",
                principalColumn: "HandsetID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPExtensions_RatePlans_RatePlanMixnMatchID",
                table: "IPExtensions",
                column: "RatePlanMixnMatchID",
                principalTable: "RatePlans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
