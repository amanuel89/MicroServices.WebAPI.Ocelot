using System;
using System.ComponentModel.DataAnnotations;

namespace CommonService.Application.Models
{
    public class ExchangeRateCreateRequest
    {
        [Required(ErrorMessage = "Currency ID is required.")]
        public long CurrencyId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Buying rate is required.")]
        public decimal Buying { get; set; }

        [Required(ErrorMessage = "Selling rate is required.")]
        public decimal Selling { get; set; }
    }

    public class ExchangeRateUpdateRequest : ExchangeRateCreateRequest
    {
        [Required(ErrorMessage = "Id is required for update.")]
        public long Id { get; set; }
    }
}
