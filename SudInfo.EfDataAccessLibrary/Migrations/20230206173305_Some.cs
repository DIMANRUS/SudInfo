using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Some : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComputerId",
                table: "Monitors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_ComputerId",
                table: "Monitors",
                column: "ComputerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitors_Computers_ComputerId",
                table: "Monitors",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitors_Computers_ComputerId",
                table: "Monitors");

            migrationBuilder.DropIndex(
                name: "IX_Monitors_ComputerId",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "ComputerId",
                table: "Monitors");
        }
    }
}
