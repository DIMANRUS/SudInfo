#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    public partial class AddNewFieldsInPrinter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cabinet",
                table: "Printers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Printers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Printers_EmployeeId",
                table: "Printers",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_Employees_EmployeeId",
                table: "Printers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Employees_EmployeeId",
                table: "Printers");

            migrationBuilder.DropIndex(
                name: "IX_Printers_EmployeeId",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "Cabinet",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Printers");
        }
    }
}
