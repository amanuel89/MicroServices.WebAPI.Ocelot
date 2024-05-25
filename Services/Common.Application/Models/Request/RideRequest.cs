using System;
using System.ComponentModel.DataAnnotations;

namespace RideBackend.Domain.Dtos
{
    public class CreateRideRequestDto
    {
        [Required(ErrorMessage = "User is required")]
        public long UserId { get; set; } 

        [Required(ErrorMessage = "Vehicle Type is required")]
        public long VehicleTypeId { get; set; }

        [Required(ErrorMessage = "Pickup Longitude is required")]
        public double PickupLongitude { get; set; }

        [Required(ErrorMessage = "Pickup Latitude is required")]
        public double PickupLatitude { get; set; }

        [Required(ErrorMessage = "Drop-Off Longitude is required")]
        public double DropOffLongitude { get; set; }

        [Required(ErrorMessage = "Drop-Off Latitude is required")]
        public double DropOffLatitude { get; set; }
    }

    public class CreateRideByDriverRequestDto
    {
        [Required(ErrorMessage = "User is required")]
        public long DriverId { get; set; }

        [Required(ErrorMessage = "Vehicle Type is required")]
        public long VehicleTypeId { get; set; }

        [Required(ErrorMessage = "PhoneNumber Type is required")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pickup Longitude is required")]
        public double PickupLongitude { get; set; }

        [Required(ErrorMessage = "Pickup Latitude is required")]
        public double PickupLatitude { get; set; }

        [Required(ErrorMessage = "Drop-Off Longitude is required")]
        public double DropOffLongitude { get; set; }

        [Required(ErrorMessage = "Drop-Off Latitude is required")]
        public double DropOffLatitude { get; set; }
    }


    public class AcceptRideRequestDto
    {
        [Required(ErrorMessage = "Driver is required")]
        public long DriverId { get; set; }

        [Required(ErrorMessage = "Order is required")]
        public long OrderId { get; set; }
    }

    public class CompleteRideRequestDto
    {
        [Required(ErrorMessage = "Driver is required")]
        public long DriverId { get; set; }

        [Required(ErrorMessage = "Order is required")]
        public long OrderId { get; set; }
        [Required(ErrorMessage = "Distance is required")]
        public long DistanceDriven { get; set; }
    }

    public class StartRideRequestDto
    {
        [Required(ErrorMessage = "Order is required")]
        public long OrderId { get; set; }

    }
}
