using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Common.Currency")]
    public class Currency : BaseEntity
    {
        public long? Country { get; private set; }
        [MaxLength(100)]
        public string Description { get; private set; } = string.Empty;
        [MaxLength(50)]
        public string Sign { get; private set; } = string.Empty;
        [MaxLength(10)]
        public string Abbreviation { get; private set; } = string.Empty;
        public bool? IsDefault { get; private set; }

        public static Currency Create(
            long? country,
            string description,
            string sign,
            string abbreviation,
            bool? isDefault)
        {
            return new Currency
            {
                Country = country,
                Description = description,
                Sign = sign,
                Abbreviation = abbreviation,
                IsDefault = isDefault,
            };
        }
    }
}
