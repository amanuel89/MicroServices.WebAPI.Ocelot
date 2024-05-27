namespace CommonService.Application.Models
{

    public class PassengerResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public double? CurrentLat { get; set; }
        public double? CurrentLng { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
