using RideBackend.Domain.Common;

namespace RideBackend.Domain.Models
{
    public class Passenger : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public bool IsVerified { get; private set; } =false;
        public string ProfilePictureUrl { get; private set; } = string.Empty;
        public double? CurrentLat { get; private set; } = 0;
        public double? CurrentLng { get; private set; } = 0;

        public static Passenger Create(string name,string email,string password,string phone, string profilePictureUrl)
        {
            return new Passenger
            {
                Name = name,
                Email = email,
                Password = password,
                Phone = phone,
                ProfilePictureUrl = profilePictureUrl
            };
        }
        public void Update(string name,string email,string profilePictureUrl)
        {
            this.Name = name;
            this.Email = email;
            this.ProfilePictureUrl = profilePictureUrl;
        }

        public void Verify(bool isVerified)
        {
            this.IsVerified = isVerified;
        }
    }
}
