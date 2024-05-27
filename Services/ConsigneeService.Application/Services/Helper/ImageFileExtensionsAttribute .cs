using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class ImageFileExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _allowedExtensions = { ".png", ".jpg", ".jpeg", ".webp" };

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (Array.IndexOf(_allowedExtensions, fileExtension) == -1)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}