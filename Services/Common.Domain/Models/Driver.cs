using RideBackend.Domain.Common;
using System.Collections.Generic;

namespace RideBackend.Domain.Models
{
    public class Driver : BaseEntity
    {
        public string DriverUserName { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string MiddleName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string DrivingLicense { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string TradeLicenseAndTin { get; private set; } = string.Empty;
        public string Photo { get; private set; } = string.Empty;
        public Gender Gender { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public bool IsOnline { get; private set; } = true;
        public bool IsAvailable { get; private set; } = false;
        public bool TripStatus { get; private set; } = false;
        public double Balance { get; private set; } = 0;
        public string Lat { get; private set; } = string.Empty;
        public string Lng { get; private set; } = string.Empty;
        public bool Notification { get; private set; }
        public DriverStatus DriverStatus { get; private set; } = DriverStatus.PENDING;
        //public string VehicleType { get; private set; } = string.Empty;
      //  public virtual Referral Referral { get; private set; }
        public virtual List<Vehicle> Vehicle { get;  set; }
        public virtual List<Transaction> Transaction { get;  set; }

        public static Driver Create(
            string username,
            string firstName,
            string middleName,
            string lastName,
            string drivingLicense,
            string phoneNumber,
            string tradeLicenseAndTin,
            string photo,
            Gender gender,
            string email)
        {
            return new Driver
            {
                DriverUserName = username,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                DrivingLicense = drivingLicense,
                PhoneNumber = phoneNumber,
                TradeLicenseAndTin = tradeLicenseAndTin,
                Photo = photo,
                Gender = gender,
                Email = email
              
            };
        }
        public void Update(
            string firstName,
            string middleName,
            string lastName,
            string drivingLicense,
            string tradeLicenseAndTin,
            string photo,
            Gender gender,
            string email)
        {
            this.DrivingLicense = drivingLicense;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.Photo = photo;
            this.Gender = gender;
            this.Email = email;
            this.TradeLicenseAndTin=tradeLicenseAndTin;
        }


    }
}
