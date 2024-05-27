using System.ComponentModel.DataAnnotations;

namespace ConsigneeService.Domain.Dtos
{
    public class SettingRequestDto : SettingRequestBaseDto
    {
     
    }

    public class SettingRequestUpdateDto : SettingRequestBaseDto
    {

    }

    public class SettingRequestBaseDto
    {
        [Required(ErrorMessage = "MaximumDriverDistanceKm is required")]
        public long MaximumDriverDistanceKm { get; set; }

        [Required(ErrorMessage = "DriverLocationUpdateIntervalMs is required")]
        public long DriverLocationUpdateIntervalMs { get; set; }

        [Required(ErrorMessage = "DriverInactivityTimeoutMs is required")]
        public long DriverInactivityTimeoutMs { get; set; }

        [Required(ErrorMessage = "ScheduledRides is required")]
        public bool ScheduledRides { get; set; }

        [Required(ErrorMessage = "CallCenterNumber is required")]
        public string CallCenterNumber { get; set; }

        [Required(ErrorMessage = "MaximumPendingBookings is required")]
        public long MaximumPendingBookings { get; set; }

        [Required(ErrorMessage = "MinimumBookingIntervalMn is required")]
        public long MinimumBookingIntervalMn { get; set; }

        [Required(ErrorMessage = "DriverDefaultCommission is required")]
        public double DriverDefaultCommission { get; set; }

        [Required(ErrorMessage = "DriverMinimumWalletBalance is required")]
        public double DriverMinimumWalletBalance { get; set; }

        [Required(ErrorMessage = "DriverMinimumWithdrawalBalanceAmount is required")]
        public double DriverMinimumWithdrawalBalanceAmount { get; set; }
    }
}
