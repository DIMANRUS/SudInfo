using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberCasbinet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cabinet",
                table: "Printers",
                newName: "NumberCabinet");

            migrationBuilder.AddColumn<int>(
                name: "NumberCabinet",
                table: "Peripheries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberCabinet",
                table: "Peripheries");

            migrationBuilder.RenameColumn(
                name: "NumberCabinet",
                table: "Printers",
                newName: "Cabinet");
        }
    }
}
