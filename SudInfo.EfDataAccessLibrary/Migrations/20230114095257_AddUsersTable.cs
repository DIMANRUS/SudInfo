using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Employees_EmployeeId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Monitors_Employees_EmployeeId",
                table: "Monitors");

            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Employees_EmployeeId",
                table: "Printers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NumberCabinet = table.Column<byte>(type: "tinyint", nullable: false),
                    PersonalPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    WorkPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PhoneLocal = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Users_EmployeeId",
                table: "Computers",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitors_Users_EmployeeId",
                table: "Monitors",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_Users_EmployeeId",
                table: "Printers",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Users_EmployeeId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Monitors_Users_EmployeeId",
                table: "Monitors");

            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Users_EmployeeId",
                table: "Printers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NumberCabinet = table.Column<byte>(type: "tinyint", nullable: false),
                    PersonalPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PhoneLocal = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    WorkPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Employees_EmployeeId",
                table: "Computers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitors_Employees_EmployeeId",
                table: "Monitors",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_Employees_EmployeeId",
                table: "Printers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
