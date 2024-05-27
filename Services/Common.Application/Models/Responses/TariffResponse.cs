namespace CommonService.Application.Models
{

    public class TariffResponseDto
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public double CostPerKm { get; set; } = 0;
        public double CostPerMinute { get; set; } = 0;
        public double PickupCost { get; set; } = 0;
        public double DropOffCost { get; set; } = 0;
        public double CancelCost { get; set; } = 0;
        public double NightCostPerKm { get; set; } = 0;
        public double NightCostPerMinute { get; set; } = 0;
        public double NightPickupCost { get; set; }
        public double NightDropOffCost { get; set; }
        public double NightCancelCost { get; set; }
        public DateTime DayStartsOn { get; set; }
        public DateTime DayEndsOn { get; set; }
        public DateTime NightStartsOn { get; set; }
        public DateTime NightEndsOn { get; set; }
        public RecordStatus RecordStatus { get; set; }

    }

 
}
