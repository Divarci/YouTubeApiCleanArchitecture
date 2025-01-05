using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeApiCleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CustomerDeleteBehaviourChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e8c2030-7e8c-4436-96a2-92dee907be1d"),
                column: "ConcurrencyStamp",
                value: "edc077d7-2275-42bf-84bc-e358deafc948");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b61f0859-cf2e-47f9-8b94-86cbee824344"),
                column: "ConcurrencyStamp",
                value: "e88cc2ac-7a7b-4fe4-940e-7451c06eb40e");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e8c2030-7e8c-4436-96a2-92dee907be1d"),
                column: "ConcurrencyStamp",
                value: "ff52dfa7-d39c-45cc-aaa3-c74c23b06bc9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b61f0859-cf2e-47f9-8b94-86cbee824344"),
                column: "ConcurrencyStamp",
                value: "e6995c25-d137-4e11-9e86-de56b2524f90");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
