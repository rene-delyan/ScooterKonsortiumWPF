using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterKonsortium.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chargingStations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PosX = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: -1),
                    PosY = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: -1),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 5),
                    UsedSlots = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chargingStations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CostPerKm = table.Column<double>(type: "REAL", nullable: false, defaultValue: 0.58999999999999997),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Hotline = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "scooters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    PosX = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: -1),
                    PosY = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: -1),
                    Battery = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 100),
                    Revenue = table.Column<double>(type: "REAL", nullable: false, defaultValue: 0.0),
                    DistanceKm = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    State = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    AtStationId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scooters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_scooters_chargingStations_AtStationId",
                        column: x => x.AtStationId,
                        principalTable: "chargingStations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_scooters_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_companies_Name",
                table: "companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_scooters_AtStationId",
                table: "scooters",
                column: "AtStationId");

            migrationBuilder.CreateIndex(
                name: "IX_scooters_CompanyId",
                table: "scooters",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scooters");

            migrationBuilder.DropTable(
                name: "chargingStations");

            migrationBuilder.DropTable(
                name: "companies");
        }
    }
}
