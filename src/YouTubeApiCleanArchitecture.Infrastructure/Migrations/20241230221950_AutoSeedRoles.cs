using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YouTubeApiCleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AutoSeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0e8c2030-7e8c-4436-96a2-92dee907be1d"), "ff52dfa7-d39c-45cc-aaa3-c74c23b06bc9", "User", "USER" },
                    { new Guid("b61f0859-cf2e-47f9-8b94-86cbee824344"), "e6995c25-d137-4e11-9e86-de56b2524f90", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e8c2030-7e8c-4436-96a2-92dee907be1d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b61f0859-cf2e-47f9-8b94-86cbee824344"));
        }
    }
}
