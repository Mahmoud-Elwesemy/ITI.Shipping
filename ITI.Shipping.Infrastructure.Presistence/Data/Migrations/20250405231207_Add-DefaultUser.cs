using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITI.Shipping.Infrastructure.Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "Permissions:ViewPermissions", "admin-role" },
                    { 2, "permissions", "Permissions:AddPermissions", "admin-role" },
                    { 3, "permissions", "Permissions:UpdatePermissions", "admin-role" },
                    { 4, "permissions", "Permissions:DeletePermissions", "admin-role" },
                    { 5, "permissions", "Settings:ViewSettings", "admin-role" },
                    { 6, "permissions", "Settings:AddSettings", "admin-role" },
                    { 7, "permissions", "Settings:UpdateSettings", "admin-role" },
                    { 8, "permissions", "Settings:DeleteSettings", "admin-role" },
                    { 9, "permissions", "Bank:ViewBank", "admin-role" },
                    { 10, "permissions", "Bank:AddBank", "admin-role" },
                    { 11, "permissions", "Bank:UpdateBank", "admin-role" },
                    { 12, "permissions", "Bank:DeleteBank", "admin-role" },
                    { 13, "permissions", "MoneySafe:ViewMoneySafe", "admin-role" },
                    { 14, "permissions", "MoneySafe:AddMoneySafe", "admin-role" },
                    { 15, "permissions", "MoneySafe:UpdateMoneySafe", "admin-role" },
                    { 16, "permissions", "MoneySafe:DeleteMoneySafe", "admin-role" },
                    { 17, "permissions", "Branches:ViewBranches", "admin-role" },
                    { 18, "permissions", "Branches:AddBranches", "admin-role" },
                    { 19, "permissions", "Branches:UpdateBranches", "admin-role" },
                    { 20, "permissions", "Branches:DeleteBranches", "admin-role" },
                    { 21, "permissions", "Employees:ViewEmployees", "admin-role" },
                    { 22, "permissions", "Employees:AddEmployees", "admin-role" },
                    { 23, "permissions", "Employees:UpdateEmployees", "admin-role" },
                    { 24, "permissions", "Employees:DeleteEmployees", "admin-role" },
                    { 25, "permissions", "Merchants:ViewMerchants", "admin-role" },
                    { 26, "permissions", "Merchants:AddMerchants", "admin-role" },
                    { 27, "permissions", "Merchants:UpdateMerchants", "admin-role" },
                    { 28, "permissions", "Merchants:DeleteMerchants", "admin-role" },
                    { 29, "permissions", "Couriers:ViewCouriers", "admin-role" },
                    { 30, "permissions", "Couriers:AddCouriers", "admin-role" },
                    { 31, "permissions", "Couriers:UpdateCouriers", "admin-role" },
                    { 32, "permissions", "Couriers:DeleteCouriers", "admin-role" },
                    { 33, "permissions", "Regions:ViewRegions", "admin-role" },
                    { 34, "permissions", "Regions:AddRegions", "admin-role" },
                    { 35, "permissions", "Regions:UpdateRegions", "admin-role" },
                    { 36, "permissions", "Regions:DeleteRegions", "admin-role" },
                    { 37, "permissions", "Cities:ViewCities", "admin-role" },
                    { 38, "permissions", "Cities:AddCities", "admin-role" },
                    { 39, "permissions", "Cities:UpdateCities", "admin-role" },
                    { 40, "permissions", "Cities:DeleteCities", "admin-role" },
                    { 41, "permissions", "Orders:ViewOrders", "admin-role" },
                    { 42, "permissions", "Orders:AddOrders", "admin-role" },
                    { 43, "permissions", "Orders:UpdateOrders", "admin-role" },
                    { 44, "permissions", "Orders:DeleteOrders", "admin-role" },
                    { 45, "permissions", "OrderReports:ViewOrderReports", "admin-role" },
                    { 46, "permissions", "OrderReports:AddOrderReports", "admin-role" },
                    { 47, "permissions", "OrderReports:UpdateOrderReports", "admin-role" },
                    { 48, "permissions", "OrderReports:DeleteOrderReports", "admin-role" },
                    { 49, "permissions", "Accounts:ViewAccounts", "admin-role" },
                    { 50, "permissions", "Accounts:AddAccounts", "admin-role" },
                    { 51, "permissions", "Accounts:UpdateAccounts", "admin-role" },
                    { 52, "permissions", "Accounts:DeleteAccounts", "admin-role" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BranchId", "CanceledOrder", "CityId", "ConcurrencyStamp", "CreatedAt", "DeductionCompanyFromOrder", "DeductionTypes", "Email", "EmailConfirmed", "FullName", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PickupPrice", "RegionId", "SecurityStamp", "StoreName", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0195d439-9ca1-7873-9c14-a4bc1c201593", 0, null, null, null, null, "0195d43b-a808-757b-9c3e-bf90c6091133", new DateTime(2025, 4, 6, 1, 12, 6, 562, DateTimeKind.Local).AddTicks(1201), null, null, "Weso430@gmail.com", false, "Weso Admin", false, false, null, "WESO430@GMAIL.COM", "WESO430@GMAIL.COM", "AQAAAAIAAYagAAAAEBs9I9gUurwErnJFl0eE0g7ghb3C5zUhQMC40OtMW8AxLlTr4PwOjiVRy/nxTkRCbg==", null, false, null, null, "0195d43be3f271878cc37be7dfc34361", null, false, "Weso430@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "admin-role", "0195d439-9ca1-7873-9c14-a4bc1c201593" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "admin-role", "0195d439-9ca1-7873-9c14-a4bc1c201593" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593");
        }
    }
}
