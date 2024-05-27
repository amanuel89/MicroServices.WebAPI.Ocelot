using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ConsigneeService.Application.Models
{
    public class BankRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account Name is required")]
        public string AccountName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account Number is required")]
        public string AccountNumber { get; set; } = string.Empty;

        [ImageFileExtensions(ErrorMessage = "Bank Logo must be in png, jpg, or webp format")]
        public IFormFile? BankLogo { get; set; }
    }


    public class BankUpdateDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account Name is required")]
        public string AccountName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account Number is required")]
        public string AccountNumber { get; set; } = string.Empty;

        public IFormFile? BankLogo { get; set; }
    }

}
