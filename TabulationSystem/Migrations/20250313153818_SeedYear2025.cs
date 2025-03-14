using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabulationSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedYear2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Years",
                columns: new[] { "YearId", "DateCreated", "DateUpdated", "Status", "YearNumber" },
                values: new object[] { 2025, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2025 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Years",
                keyColumn: "YearId",
                keyValue: 2025);
        }
    }
}
