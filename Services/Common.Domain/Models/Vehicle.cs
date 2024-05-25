using RideBackend.Domain.Common;

namespace RideBackend.Domain.Models
{
    public class Vehicle : BaseEntity
    {
        public string VehicleMaker { get; private set; } = string.Empty;
        public string Model { get; private set; } = string.Empty;
        public string ManufacturingYear { get; private set; } = string.Empty;
        public long VehicleTypeId { get; private set; }
        public string PlateNumber { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public string SideOne { get; private set; } = string.Empty;
        public string SideTwo { get; private set; } = string.Empty;
        public string Front { get; private set; } = string.Empty;
        public string Rear { get; private set; } = string.Empty;
        public string Insurance { get; private set; } = string.Empty;
        public string PowerOfAttorney { get; private set; } = string.Empty;
        public bool IsOwner { get; private set; }
        public string Libre { get; private set; } = string.Empty;
        public long? DriverId { get; private set; }

        public virtual VehicleTypes VehicleType { get;  set; }


        public static Vehicle Create(
            string vehicleMaker,
            string model,
            string manufacturingYear,
            long vehicleTypeId,
            string plateNumber,
            string color,
            string sideOne,
            string sideTwo,
            string front,
            string rear,
            string insurance,
            string powerOfAttorney,
            bool isOwner,
            string libre)
        {
            return new Vehicle
            {
                VehicleMaker = vehicleMaker,
                Model = model,
                ManufacturingYear = manufacturingYear,
                VehicleTypeId = vehicleTypeId,
                PlateNumber = plateNumber,
                Color = color,
                SideOne = sideOne,
                SideTwo = sideTwo,
                Front = front,
                Rear = rear,
                Insurance = insurance,
                PowerOfAttorney = powerOfAttorney,
                IsOwner = isOwner,
                Libre = libre        
            };
        }
    }
}
