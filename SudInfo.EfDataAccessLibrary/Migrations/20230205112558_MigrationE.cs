using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class MigrationE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComputerId",
                table: "Peripheries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Peripheries_ComputerId",
                table: "Peripheries",
                column: "ComputerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peripheries_Computers_ComputerId",
                table: "Peripheries",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peripheries_Computers_ComputerId",
                table: "Peripheries");

            migrationBuilder.DropIndex(
                name: "IX_Peripheries_ComputerId",
                table: "Peripheries");

            migrationBuilder.DropColumn(
                name: "ComputerId",
                table: "Peripheries");
        }
    }
}
