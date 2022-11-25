#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    public partial class AddGpuToComputer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GPU",
                table: "Computers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GPU",
                table: "Computers");
        }
    }
}
