using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peripheries_Users_UserId",
                table: "Peripheries");

            migrationBuilder.DropIndex(
                name: "IX_Peripheries_UserId",
                table: "Peripheries");

            migrationBuilder.DropColumn(
                name: "IsDecommissioned",
                table: "Peripheries");

            migrationBuilder.DropColumn(
                name: "NumberCabinet",
                table: "Peripheries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Peripheries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDecommissioned",
                table: "Peripheries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberCabinet",
                table: "Peripheries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Peripheries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Peripheries_UserId",
                table: "Peripheries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peripheries_Users_UserId",
                table: "Peripheries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
