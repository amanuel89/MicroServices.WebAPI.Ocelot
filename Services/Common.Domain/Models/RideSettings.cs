using RideBackend.Domain.Common;

namespace RideBackend.Domain.Models
{
    public class RideSettings : BaseEntity
    {
        public long MaximumDriverDistanceKm { get; set; }
        public long DriverLocationUpdateIntervalMs { get; set; }
        public long DriverInactivityTimeoutMs { get; set; }
        public bool ScheduledRides { get; set; }
        public string CallCenterNumber { get; set; } = string.Empty;
        public long MaximumPendingBookings { get; set; }
        public long MinimumBookingIntervalMn { get; set; }
        public double DriverDefaultCommission { get; set; }
        public double DriverMinimumWalletBalance { get; set; }
        public double DriverMinimumWithdrawalBalanceAmount { get; set; }

        public static RideSettings Create(
            long maximumDriverDistanceKm,
            long driverLocationUpdateIntervalMs,
            long driverInactivityTimeoutMs,
            bool scheduledRides,
            string callCenterNumber,
            long maximumPendingBookings,
            long minimumBookingIntervalMn,
            double driverDefaultCommission,
            double driverMinimumWalletBalance,
            double driverMinimumWithdrawalBalanceAmount)
        {
            return new RideSettings
            {
                MaximumDriverDistanceKm = maximumDriverDistanceKm,
                DriverLocationUpdateIntervalMs = driverLocationUpdateIntervalMs,
                DriverInactivityTimeoutMs = driverInactivityTimeoutMs,
                ScheduledRides = scheduledRides,
                CallCenterNumber = callCenterNumber,
                MaximumPendingBookings = maximumPendingBookings,
                MinimumBookingIntervalMn = minimumBookingIntervalMn,
                DriverDefaultCommission = driverDefaultCommission,
                DriverMinimumWalletBalance = driverMinimumWalletBalance,
                DriverMinimumWithdrawalBalanceAmount = driverMinimumWithdrawalBalanceAmount
            };
        }

        public void Update(
            long maximumDriverDistanceKm,
            long driverLocationUpdateIntervalMs,
            long driverInactivityTimeoutMs,
            bool scheduledRides,
            string callCenterNumber,
            long maximumPendingBookings,
            long minimumBookingIntervalMn,
            double driverDefaultCommission,
            double driverMinimumWalletBalance,
            double driverMinimumWithdrawalBalanceAmount)
        {
            MaximumDriverDistanceKm = maximumDriverDistanceKm;
            DriverLocationUpdateIntervalMs = driverLocationUpdateIntervalMs;
            DriverInactivityTimeoutMs = driverInactivityTimeoutMs;
            ScheduledRides = scheduledRides;
            CallCenterNumber = callCenterNumber;
            MaximumPendingBookings = maximumPendingBookings;
            MinimumBookingIntervalMn = minimumBookingIntervalMn;
            DriverDefaultCommission = driverDefaultCommission;
            DriverMinimumWalletBalance = driverMinimumWalletBalance;
            DriverMinimumWithdrawalBalanceAmount = driverMinimumWithdrawalBalanceAmount;
        }
    }
}
