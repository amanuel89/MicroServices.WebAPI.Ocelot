using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class vehicleup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicle_DriverId",
                table: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_DriverId",
                table: "Vehicle",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicle_DriverId",
                table: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_DriverId",
                table: "Vehicle",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");
        }
    }
}
