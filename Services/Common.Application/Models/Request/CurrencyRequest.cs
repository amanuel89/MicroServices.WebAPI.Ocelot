using System.ComponentModel.DataAnnotations;

namespace CommonService.Application.Models
{
    public class CurrencyCreateRequest
    {
        [Required()]
        public long? CountryId { get; set; }

        [Required()]
        [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
        public string Description { get; set; } = string.Empty;

        [Required()]
        [MaxLength(50, ErrorMessage = "Sign cannot exceed 50 characters")]
        public string Sign { get; set; } = string.Empty;

        [Required()]
        [MaxLength(10, ErrorMessage = "Abbreviation cannot exceed 10 characters")]
        public string Abbreviation { get; set; } = string.Empty;

        [Required()]
        public bool? IsDefault { get; set; }
    }

    public class CurrencyUpdateRequest : CurrencyCreateRequest
    {
        [Required(ErrorMessage = "Id is required for update.")]
        public long Id { get; set; }
    }
}
