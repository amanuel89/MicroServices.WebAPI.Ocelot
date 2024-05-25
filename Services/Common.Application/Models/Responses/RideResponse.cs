namespace RideBackend.Application.Models
{

    public class RideResponseDto
    {
        public long Id { get; set; }
        public long? DriverId { get; set; } = null;
        public long UserId { get; set; }
        public long VehicleTypeId { get; set; }
        public double PickupLongitude { get; set; }
        public double PickupLatitude { get; set; }
        public double DropOffLongitude { get; set; }
        public double DropOffLatitude { get; set; }
        public double Price { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public DateTime AcceptedTime { get; set; } = DateTime.MaxValue;
        public DateTime RideStartedTime { get; set; } = DateTime.MaxValue;
        public DateTime OrderCompletedTime { get; set; } = DateTime.MaxValue;
        public string CancelledReason { get; set; } = string.Empty;
        public RideStatus RideStatus { get; set; } = RideStatus.Pending;
        public RecordStatus RecordStatus { get; set; }

    }

    public class RideNotificationDto
    {
        public long Id { get; set; }
        public double PickupLongitude { get; set; }
        public double PickupLatitude { get; set; }
        public double DropOffLongitude { get; set; }
        public double DropOffLatitude { get; set; }
        public double Price { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public DateTime OrderTime { get; set; } = DateTime.Now; 

    }
}
