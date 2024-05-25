using System;
using System.ComponentModel.DataAnnotations;

namespace RideBackend.Domain.Dtos
{
    public class TariffRequesDto : TariffUpdateRequesDto
    {
        [Required(ErrorMessage = "VehicleId is required.")]
        public long VehicleId { get; set; }
    }

    public class TariffUpdateRequesDto
    {

        [Required(ErrorMessage = "CostPerKm is required.")]
        public double CostPerKm { get; set; } = 0;

        [Required(ErrorMessage = "CostPerMinute is required.")]
        public double CostPerMinute { get; set; } = 0;

        [Required(ErrorMessage = "PickupCost is required.")]
        public double PickupCost { get; set; } = 0;

        [Required(ErrorMessage = "DropOffCost is required.")]
        public double DropOffCost { get; set; } = 0;

        [Required(ErrorMessage = "CancelCost is required.")]
        public double CancelCost { get; set; } = 0;

        [Required(ErrorMessage = "NightCostPerKm is required.")]
        public double NightCostPerKm { get; set; } = 0;

        [Required(ErrorMessage = "NightCostPerMinute is required.")]
        public double NightCostPerMinute { get; set; } = 0;

        [Required(ErrorMessage = "NightPickupCost is required.")]
        public double NightPickupCost { get; set; }

        [Required(ErrorMessage = "NightDropOffCost is required.")]
        public double NightDropOffCost { get; set; }

        [Required(ErrorMessage = "NightCancelCost is required.")]
        public double NightCancelCost { get; set; }

        [Required(ErrorMessage = "DayStartsOn is required.")]
        public DateTime DayStartsOn { get; set; }

        [Required(ErrorMessage = "DayEndsOn is required.")]
        public DateTime DayEndsOn { get; set; }

        [Required(ErrorMessage = "NightStartsOn is required.")]
        public DateTime NightStartsOn { get; set; }

        [Required(ErrorMessage = "NightEndsOn is required.")]
        public DateTime NightEndsOn { get; set; }
    }
}
