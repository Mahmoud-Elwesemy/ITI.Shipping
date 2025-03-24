using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI.Shipping.Infrastructure.Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedOrdertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "Orders",
                type: "int",
                nullable: true);
        }
    }
}
