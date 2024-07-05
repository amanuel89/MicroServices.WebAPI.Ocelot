using System.ComponentModel.DataAnnotations;

namespace CommonService.Application.Models
{
    public class SystemLookupCreateRequest
    {
        [Required(ErrorMessage = "Index is required.")]
        public int Index { get; set; }

        [Required(ErrorMessage = "IsSystemDefined is required.")]
        public bool IsSystemDefined { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [MaxLength(26, ErrorMessage = "Type cannot exceed 26 characters.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Value is required.")]
        [MaxLength(200, ErrorMessage = "Value cannot exceed 200 characters.")]
        public string Value { get; set; }

        [Required(ErrorMessage = "IsDefault is required.")]
        public bool IsDefault { get; set; }
    }

    public class SystemLookupUpdateRequest : SystemLookupCreateRequest
    {
        [Required(ErrorMessage = "Id is required for update.")]
        public int Id { get; set; }
    }
}
