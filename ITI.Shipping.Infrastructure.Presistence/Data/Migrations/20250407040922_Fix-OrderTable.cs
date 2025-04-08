using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI.Shipping.Infrastructure.Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingCost",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593",
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 4, 7, 6, 9, 21, 317, DateTimeKind.Local).AddTicks(2240), "AQAAAAIAAYagAAAAEDkwfiAN85HZq4NVHLIESJNCT9+jeZWRwSMTJ4NkrvDjChJPLCPCstbbrsF2xF4diQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593",
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 4, 6, 1, 12, 6, 562, DateTimeKind.Local).AddTicks(1201), "AQAAAAIAAYagAAAAEBs9I9gUurwErnJFl0eE0g7ghb3C5zUhQMC40OtMW8AxLlTr4PwOjiVRy/nxTkRCbg==" });
        }
    }
}
