namespace ConsigneeService.Application.Models
{

    public class SettingsResponseDto
    {
        public long Id { get; set; }
        public long MaximumDriverDistanceKm { get; set; }
        public long DriverLocationUpdateIntervalMs { get; set; }
        public long DriverInactivityTimeoutMs { get; set; }
        public bool ScheduledRides { get; set; }
        public string CallCenterNumber { get; set; } =string.Empty;
        public long MaximumPendingBookings { get; set; }
        public long MinimumBookingIntervalMn { get; set; }
        public double DriverDefaultCommission { get; set; }
        public double DriverMinimumWalletBalance { get; set; }
        public double DriverMinimumWithdrawalBalanceAmount { get; set; }
        public RecordStatus RecordStatus { get; set; }

    }

 
}
