using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterKonsortium.Migrations
{
    /// <inheritdoc />
    public partial class EditTableNamesToBeUpperCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_scooters_chargingStations_AtStationId",
                table: "scooters");

            migrationBuilder.DropForeignKey(
                name: "FK_scooters_companies_CompanyId",
                table: "scooters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_scooters",
                table: "scooters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_companies",
                table: "companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chargingStations",
                table: "chargingStations");

            migrationBuilder.RenameTable(
                name: "scooters",
                newName: "Scooters");

            migrationBuilder.RenameTable(
                name: "companies",
                newName: "Companies");

            migrationBuilder.RenameTable(
                name: "chargingStations",
                newName: "ChargingStations");

            migrationBuilder.RenameIndex(
                name: "IX_scooters_CompanyId",
                table: "Scooters",
                newName: "IX_Scooters_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_scooters_AtStationId",
                table: "Scooters",
                newName: "IX_Scooters_AtStationId");

            migrationBuilder.RenameIndex(
                name: "IX_companies_Name",
                table: "Companies",
                newName: "IX_Companies_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scooters",
                table: "Scooters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChargingStations",
                table: "ChargingStations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scooters_ChargingStations_AtStationId",
                table: "Scooters",
                column: "AtStationId",
                principalTable: "ChargingStations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scooters_Companies_CompanyId",
                table: "Scooters",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scooters_ChargingStations_AtStationId",
                table: "Scooters");

            migrationBuilder.DropForeignKey(
                name: "FK_Scooters_Companies_CompanyId",
                table: "Scooters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scooters",
                table: "Scooters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChargingStations",
                table: "ChargingStations");

            migrationBuilder.RenameTable(
                name: "Scooters",
                newName: "scooters");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "companies");

            migrationBuilder.RenameTable(
                name: "ChargingStations",
                newName: "chargingStations");

            migrationBuilder.RenameIndex(
                name: "IX_Scooters_CompanyId",
                table: "scooters",
                newName: "IX_scooters_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Scooters_AtStationId",
                table: "scooters",
                newName: "IX_scooters_AtStationId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_Name",
                table: "companies",
                newName: "IX_companies_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_scooters",
                table: "scooters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_companies",
                table: "companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chargingStations",
                table: "chargingStations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_scooters_chargingStations_AtStationId",
                table: "scooters",
                column: "AtStationId",
                principalTable: "chargingStations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_scooters_companies_CompanyId",
                table: "scooters",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
