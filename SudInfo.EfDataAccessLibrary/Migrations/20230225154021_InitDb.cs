using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServerRacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    InventarNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerRacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NumberCabinet = table.Column<int>(type: "int", nullable: false),
                    PersonalPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    WorkPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    PhoneLocal = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InventarNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PosiitionInServerRack = table.Column<int>(type: "int", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ServerRackId = table.Column<int>(type: "int", nullable: true),
                    OperatingSystem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_ServerRacks_ServerRackId",
                        column: x => x.ServerRackId,
                        principalTable: "ServerRacks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OS = table.Column<int>(type: "int", nullable: false),
                    CPU = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    GPU = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ROM = table.Column<int>(type: "int", nullable: false),
                    RAM = table.Column<int>(type: "int", nullable: false),
                    NumberCabinet = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InventarNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    YearRelease = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rutokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EndDateCertificate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rutokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Monitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ScreenSize = table.Column<int>(type: "int", nullable: false),
                    ScreenResolutionWidth = table.Column<int>(type: "int", nullable: false),
                    ScreenResolutionHeight = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InventarNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    YearRelease = table.Column<int>(type: "int", nullable: false),
                    ComputerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monitors_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Peripheries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    InventarNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ComputerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peripheries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peripheries_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    NumberCabinet = table.Column<int>(type: "int", nullable: false),
                    YearRelease = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InventarNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ComputerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Printers_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Computers_UserId",
                table: "Computers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_ComputerId",
                table: "Monitors",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_Peripheries_ComputerId",
                table: "Peripheries",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_Printers_ComputerId",
                table: "Printers",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rutokens_UserId",
                table: "Rutokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerRacks_Position",
                table: "ServerRacks",
                column: "Position",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servers_ServerRackId",
                table: "Servers",
                column: "ServerRackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitors");

            migrationBuilder.DropTable(
                name: "Peripheries");

            migrationBuilder.DropTable(
                name: "Printers");

            migrationBuilder.DropTable(
                name: "Rutokens");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropTable(
                name: "ServerRacks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
