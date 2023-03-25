using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddAppsToComputer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Apps_AppEntityId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_AppEntityId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "AppEntityId",
                table: "Computers");

            migrationBuilder.CreateTable(
                name: "AppEntityComputer",
                columns: table => new
                {
                    AppsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEntityComputer", x => new { x.AppsId, x.ComputersId });
                    table.ForeignKey(
                        name: "FK_AppEntityComputer_Apps_AppsId",
                        column: x => x.AppsId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppEntityComputer_Computers_ComputersId",
                        column: x => x.ComputersId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppEntityComputer_ComputersId",
                table: "AppEntityComputer",
                column: "ComputersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEntityComputer");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "AppEntityId",
                table: "Computers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computers_AppEntityId",
                table: "Computers",
                column: "AppEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Apps_AppEntityId",
                table: "Computers",
                column: "AppEntityId",
                principalTable: "Apps",
                principalColumn: "Id");
        }
    }
}
