using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RideBackend.Application.Models
{
    public class PassengerUpdateDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "email format is invalid")]
        public string Email { get; set; } = string.Empty;
        [ImageFileExtensions(ErrorMessage = "Photo must be in png, jpg, or webp format")]
        public IFormFile? ProfilePictureUrl { get; set; }
    }

    public class PassengerRequestDTO : PassengerUpdateDTO
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = string.Empty;
    }

    //
    public class DriverCreateRequestDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Middle name cannot exceed 50 characters")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Driver Photo is required")]
        [ImageFileExtensions(ErrorMessage = "Driver photo must be in photo format with png, jpg, or webp")]
        public required IFormFile Photo { get; set; }


        [Required(ErrorMessage = "Driving license is required")]
        [ImageFileExtensions(ErrorMessage = "Driving license attachment must be in photo format with png, jpg, or webp")]
        public required IFormFile DrivingLicense { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trade license and TIN is required")]
        [StringLength(20, ErrorMessage = "Trade license and TIN cannot exceed 50 characters")]
        public string TradeLicenseAndTin { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "EnterValid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vehicle Information is Required")]
        public virtual VehicleCreateRequest? Vehicle { get; set; } = null;
    }

    public class VehicleCreateRequest
    {
        [Required(ErrorMessage = "Vehicle maker is required")]
        [StringLength(50, ErrorMessage = "Vehicle maker cannot exceed 50 characters")]
        public string VehicleMaker { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Manufacturing year is required")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Manufacturing year must be 4 digits")]
        public string ManufacturingYear { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vehicle type ID is required")]
        public long VehicleTypeId { get; set; }

        [Required(ErrorMessage = "Plate number is required")]
        [StringLength(15, ErrorMessage = "Plate number cannot exceed 15 characters")]
        public string PlateNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vehicle Color is required")]
        [StringLength(20, ErrorMessage = "Vehicle Color cannot exceed 20 characters")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Side vehicle photo required")]
        [ImageFileExtensions(ErrorMessage = "Side vehicle attachment must be in photo format with png, jpg, or webp")]
        public required IFormFile SideOne { get; set; }

        [Required(ErrorMessage = "Side vehicle required")]
        [ImageFileExtensions(ErrorMessage = "Side vehicle attachment must be in photo format with png, jpg, or webp")]
        public required IFormFile SideTwo { get; set; }

        [Required(ErrorMessage = "Front vehicle  is required")]
        [ImageFileExtensions(ErrorMessage = "Front vehicle attachment must be in photo format with png, jpg, or webp")]
        public required IFormFile Front { get; set; }

        [Required(ErrorMessage = "Rear vehicle photo is required")]
        [ImageFileExtensions(ErrorMessage = "Rear vehicle attachment must be in photo format with png, jpg, or webp")]
        public required IFormFile Rear { get; set; }

        public IFormFile? PowerOfAttorney { get; set; } = null;

        [Required(ErrorMessage = "Insurance document is required")]
        [ImageFileExtensions(ErrorMessage = "Insurance attachment must be in photo format with png, jpg, or webp")]
        public required IFormFile Insurance { get; set; }

        [Required(ErrorMessage = "Owner status is required")]
        public bool IsOwner { get; set; }

        [Required(ErrorMessage = "Libre is required")]
        [ImageFileExtensions(ErrorMessage = "Libre must be in photo format with png, jpg, or webp")]
        public required IFormFile Libre { get; set; }
    }

    public class VehicleTypesCreateDto
    {
        [Required(ErrorMessage = "Vehicle Type Name is required")]
        [StringLength(50, ErrorMessage = "Vehicle Type Name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image is required")]
        [ImageFileExtensions(ErrorMessage = "Attachment must be in photo format with png, jpg, or webp")]
        public required IFormFile Image { get; set; }

        [Required(ErrorMessage = "Number of passengers is required")]
        public int NumberOfPassengers { get; set; }

        [Required(ErrorMessage = "Rate per kilometer is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Rate per kilometer must be a positive number")]
        public decimal RatePerKm { get; set; }

        [Required(ErrorMessage = "Initial rate is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Initial rate must be a positive number")]
        public double InitialRate { get; set; }
    }

    public class VehicleTypesUpdateDto
    {
        [Required(ErrorMessage = "Vehicle Type Name is required")]
        [StringLength(50, ErrorMessage = "Vehicle Type Name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;

        [ImageFileExtensions(ErrorMessage = "Attachment must be in photo format with png, jpg, or webp")]
        public  IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Number of passengers is required")]
        public int NumberOfPassengers { get; set; }

        [Required(ErrorMessage = "Rate per kilometer is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Rate per kilometer must be a positive number")]
        public decimal RatePerKm { get; set; }

        [Required(ErrorMessage = "Initial rate is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Initial rate must be a positive number")]
        public double InitialRate { get; set; }
    }

    public class DriverUpdateRequest
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Middle name cannot exceed 50 characters")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [ImageFileExtensions(ErrorMessage = "Driver photo must be in photo format with png, jpg, or webp")]
        public IFormFile? Photo { get; set; }

        [ImageFileExtensions(ErrorMessage = "Driving license attachment must be in photo format with png, jpg, or webp")]
        public IFormFile? DrivingLicense { get; set; }

        [Required(ErrorMessage = "Trade license and TIN is required")]
        [StringLength(20, ErrorMessage = "Trade license and TIN cannot exceed 50 characters")]
        public string TradeLicenseAndTin { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "EnterValid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

    }
    public class DriverLocationUpdateRequest
    {
        public string DriverId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Heading { get; set; }
    }

}
