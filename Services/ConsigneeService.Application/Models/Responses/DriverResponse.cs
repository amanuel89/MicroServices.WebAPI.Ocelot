namespace ConsigneeService.Application.Models
{

    public class DriverResponseDTO
    {
        public long Id { get; set; }
        public string DriverUserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DrivingLicense { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string TradeLicenseAndTin { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsOnline { get; set; } = true;
        public bool IsAvailable { get; set; }
        public bool TripStatus { get; set; }
        public double Balance { get; set; }
        public string Lat { get; set; } = string.Empty;
        public string Lng { get; set; } = string.Empty;
        public bool Notification { get; set; }
        public DriverStatus DriverStatus { get; set; }
        public string VehicleType { get; set; } = string.Empty;
        public virtual List<VehicleResponseDTO> Vehicle { get; set; }
        public virtual List<Transaction> Transaction { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
