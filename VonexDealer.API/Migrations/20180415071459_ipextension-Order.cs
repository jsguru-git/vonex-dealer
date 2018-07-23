using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VonexDealer.API.Migrations
{
    public partial class ipextensionOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPExtensions_IPOrders_IPOrderID",
                table: "IPExtensions");

            migrationBuilder.DropForeignKey(
                name: "FK_IPOrders_HardwareOrder_HardwareOrderID",
                table: "IPOrders");

            migrationBuilder.DropTable(
                name: "HardwareOrderDetail");

            migrationBuilder.DropTable(
                name: "HardwareOrder");

            migrationBuilder.DropIndex(
                name: "IX_IPOrders_HardwareOrderID",
                table: "IPOrders");

            migrationBuilder.DropIndex(
                name: "IX_IPExtensions_IPOrderID",
                table: "IPExtensions");

            migrationBuilder.DropColumn(
                name: "HardwareOrderID",
                table: "IPOrders");

            migrationBuilder.RenameColumn(
                name: "IPOrderID",
                table: "IPExtensions",
                newName: "OrderID");

            migrationBuilder.AddColumn<long>(
                name: "OrderID",
                table: "IPOrders",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "IPOrders");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "IPExtensions",
                newName: "IPOrderID");

            migrationBuilder.AddColumn<long>(
                name: "HardwareOrderID",
                table: "IPOrders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HardwareOrder",
                columns: table => new
                {
                    HardwareOrderID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Freight = table.Column<double>(nullable: false),
                    OrderID = table.Column<long>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOrder", x => x.HardwareOrderID);
                });

            migrationBuilder.CreateTable(
                name: "HardwareOrderDetail",
                columns: table => new
                {
                    HardwareOrderDetailID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HandsetID = table.Column<long>(nullable: true),
                    HardwareOrderID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOrderDetail", x => x.HardwareOrderDetailID);
                    table.ForeignKey(
                        name: "FK_HardwareOrderDetail_Handsets_HandsetID",
                        column: x => x.HandsetID,
                        principalTable: "Handsets",
                        principalColumn: "HandsetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareOrderDetail_HardwareOrder_HardwareOrderID",
                        column: x => x.HardwareOrderID,
                        principalTable: "HardwareOrder",
                        principalColumn: "HardwareOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPOrders_HardwareOrderID",
                table: "IPOrders",
                column: "HardwareOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_IPExtensions_IPOrderID",
                table: "IPExtensions",
                column: "IPOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareOrderDetail_HandsetID",
                table: "HardwareOrderDetail",
                column: "HandsetID");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareOrderDetail_HardwareOrderID",
                table: "HardwareOrderDetail",
                column: "HardwareOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_IPExtensions_IPOrders_IPOrderID",
                table: "IPExtensions",
                column: "IPOrderID",
                principalTable: "IPOrders",
                principalColumn: "IPOrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPOrders_HardwareOrder_HardwareOrderID",
                table: "IPOrders",
                column: "HardwareOrderID",
                principalTable: "HardwareOrder",
                principalColumn: "HardwareOrderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
