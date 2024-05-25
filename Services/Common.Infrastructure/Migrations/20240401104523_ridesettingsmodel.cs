using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ridesettingsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RideSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaximumDriverDistanceKm = table.Column<long>(type: "bigint", nullable: false),
                    DriverLocationUpdateIntervalMs = table.Column<long>(type: "bigint", nullable: false),
                    DriverInactivityTimeoutMs = table.Column<long>(type: "bigint", nullable: false),
                    ScheduledRides = table.Column<bool>(type: "bit", nullable: false),
                    CallCenterNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaximumPendingBookings = table.Column<long>(type: "bigint", nullable: false),
                    MinimumBookingIntervalMn = table.Column<long>(type: "bigint", nullable: false),
                    DriverDefaultCommission = table.Column<double>(type: "float", nullable: false),
                    DriverMinimumWalletBalance = table.Column<double>(type: "float", nullable: false),
                    DriverMinimumWithdrawalBalanceAmount = table.Column<double>(type: "float", nullable: false),
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
                    table.PrimaryKey("PK_RideSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideSettings");
        }
    }
}
