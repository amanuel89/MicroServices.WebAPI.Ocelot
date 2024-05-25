using RideBackend.Domain.Common;
using System.Collections.Generic;

namespace RideBackend.Domain.Models
{
    public class Tariff : BaseEntity
    {
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

        public static Tariff Create(long vehicleId, double costPerKm, double costPerMinute, double pickupCost, double dropOffCost,
              double cancelCost, double nightCostPerKm, double nightCostPerMinute, double nightPickupCost, double nightDropOffCost,
              double nightCancelCost, DateTime dayStartOn, DateTime dayEndOn, DateTime nightStarton, DateTime nightEndOn)
        {
            return new Tariff
            {
                VehicleId = vehicleId,
                CostPerKm = costPerKm,
                CostPerMinute = costPerMinute,
                PickupCost = pickupCost,
                DropOffCost = dropOffCost,
                CancelCost = cancelCost,
                NightCostPerKm = nightCostPerKm,
                NightCostPerMinute = nightCostPerMinute,
                NightPickupCost = nightPickupCost,
                NightDropOffCost = nightDropOffCost,
                NightCancelCost = nightCancelCost,
                DayEndsOn = dayStartOn,
                DayStartsOn = dayEndOn,
                NightEndsOn = nightStarton,
                NightStartsOn = nightEndOn,
            };
        }

        public void Update(double costPerKm, double costPerMinute, double pickupCost, double dropOffCost,
            double cancelCost, double nightCostPerKm, double nightCostPerMinute, double nightPickupCost, double nightDropOffCost,
            double nightCancelCost, DateTime dayStartOn, DateTime dayEndOn, DateTime nightStarton, DateTime nightEndOn)
        {
            this.CostPerKm = costPerKm;
            this.CostPerMinute = costPerMinute;
            this.PickupCost = pickupCost;
            this.DropOffCost = dropOffCost;
            this.CancelCost = cancelCost;
            this.NightCostPerKm = nightCostPerKm;
            this.NightCostPerMinute = nightCostPerMinute;
            this.NightPickupCost = nightPickupCost;
            this.NightDropOffCost = nightDropOffCost;
            this.NightCancelCost = nightCancelCost;
            this.DayEndsOn = dayStartOn;
            this.DayStartsOn = dayEndOn;
            this.NightEndsOn = nightStarton;
            this.NightStartsOn = nightEndOn;
        }
    }
}
