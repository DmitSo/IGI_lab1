using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EFCore_LINQ.Migrations
{
    public partial class InitialSqlite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fuels",
                columns: table => new
                {
                    FuelID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FuelDensity = table.Column<float>(type: "REAL", nullable: false),
                    FuelType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuels", x => x.FuelID);
                });

            migrationBuilder.CreateTable(
                name: "Tanks",
                columns: table => new
                {
                    TankID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TankMaterial = table.Column<string>(type: "TEXT", nullable: true),
                    TankPicture = table.Column<string>(type: "TEXT", nullable: true),
                    TankType = table.Column<string>(type: "TEXT", nullable: true),
                    TankVolume = table.Column<float>(type: "REAL", nullable: false),
                    TankWeight = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tanks", x => x.TankID);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    OperationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FuelID = table.Column<int>(type: "INTEGER", nullable: true),
                    Inc_Exp = table.Column<float>(type: "REAL", nullable: true),
                    TankID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.OperationID);
                    table.ForeignKey(
                        name: "FK_Operations_Fuels_FuelID",
                        column: x => x.FuelID,
                        principalTable: "Fuels",
                        principalColumn: "FuelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_Tanks_TankID",
                        column: x => x.TankID,
                        principalTable: "Tanks",
                        principalColumn: "TankID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FuelID",
                table: "Operations",
                column: "FuelID");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_TankID",
                table: "Operations",
                column: "TankID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Fuels");

            migrationBuilder.DropTable(
                name: "Tanks");
        }
    }
}
