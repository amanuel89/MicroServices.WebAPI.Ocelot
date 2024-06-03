using System.ComponentModel.DataAnnotations;

namespace CommonService.Application.Models
{
    public class CountryCreateRequest
    {
        [Required(ErrorMessage = "Country Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Political Name is required")]
        [MaxLength(100, ErrorMessage = "Political Name cannot exceed 100 characters")]
        public string PoliticalName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Country Name is required")]
        public long? Continent { get; set; }
        [Required(ErrorMessage = "Telephone Code is required")]
        [MaxLength(10, ErrorMessage = "Telephone Code cannot exceed 10 characters")]
        public string TelephoneCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Time Zone  is required")]
        [MaxLength(10, ErrorMessage = "Time Zone cannot exceed 10 characters")]
        public string TimeZone { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nationality is required")]
        [MaxLength(50, ErrorMessage = "Nationality cannot exceed 50 characters")]
        public string Nationality { get; set; } = string.Empty;
        [Required(ErrorMessage = "Country Code is required")]
        [MaxLength(10, ErrorMessage = "Country Code cannot exceed 10 characters")]
        public string CountryCode { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }

    public class CountryUpdateRequest : CountryCreateRequest
    {
        [Required(ErrorMessage = "Id is required for update.")]
        public long Id { get; set; }
    }
}
