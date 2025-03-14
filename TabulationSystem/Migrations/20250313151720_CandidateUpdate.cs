using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabulationSystem.Migrations
{
    /// <inheritdoc />
    public partial class CandidateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_ApplicationUserId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Years_YearId",
                table: "Candidates");

            migrationBuilder.AlterColumn<int>(
                name: "YearId",
                table: "Candidates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Candidates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_ApplicationUserId",
                table: "Candidates",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Years_YearId",
                table: "Candidates",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "YearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_ApplicationUserId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Years_YearId",
                table: "Candidates");

            migrationBuilder.AlterColumn<int>(
                name: "YearId",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Candidates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_ApplicationUserId",
                table: "Candidates",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Years_YearId",
                table: "Candidates",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "YearId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
