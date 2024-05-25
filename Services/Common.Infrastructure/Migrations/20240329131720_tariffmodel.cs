using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tariffmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<long>(type: "bigint", nullable: false),
                    CostPerKm = table.Column<double>(type: "float", nullable: false),
                    CostPerMinute = table.Column<double>(type: "float", nullable: false),
                    PickupCost = table.Column<double>(type: "float", nullable: false),
                    DropOffCost = table.Column<double>(type: "float", nullable: false),
                    CancelCost = table.Column<double>(type: "float", nullable: false),
                    NightCostPerKm = table.Column<double>(type: "float", nullable: false),
                    NightCostPerMinute = table.Column<double>(type: "float", nullable: false),
                    NightPickupCost = table.Column<double>(type: "float", nullable: false),
                    NightDropOffCost = table.Column<double>(type: "float", nullable: false),
                    NightCancelCost = table.Column<double>(type: "float", nullable: false),
                    DayStartsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayEndsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NightStartsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NightEndsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZoneInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrnxUnit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tariffs");
        }
    }
}
