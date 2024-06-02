using CommonService.Domain.Common;

namespace CommonService.Domain.Models
{
    public class Currency : BaseEntity
    {
        public long? Country { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public string Sign { get; private set; } = string.Empty;
        public string Abbreviation { get; private set; } = string.Empty;
        public bool? IsDefault { get; private set; }
        public long? TrxnUnit { get; private set; }
        public string Remark { get; private set; } = string.Empty;

        public static Currency Create(
            long? country,
            string description,
            string sign,
            string abbreviation,
            bool? isDefault,
            long? trxnUnit,
            string remark)
        {
            return new Currency
            {
                Country = country,
                Description = description,
                Sign = sign,
                Abbreviation = abbreviation,
                IsDefault = isDefault,
                TrxnUnit = trxnUnit,
                Remark = remark
            };
        }
    }
}
