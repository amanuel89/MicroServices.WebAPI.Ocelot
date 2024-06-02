using CommonService.Domain.Common;

namespace CommonService.Domain.Models
{
    public class ExchangeRate : BaseEntity
    {
        public long? Currency { get; private set; }
        public DateTime? Date { get; private set; }
        public decimal? Buying { get; private set; }
        public decimal? Selling { get; private set; }
        public long? TxnUnit { get; private set; }
        public string Remark { get; private set; } = string.Empty;

        public static ExchangeRate Create(
            long? currency,
            DateTime? date,
            decimal? buying,
            decimal? selling,
            long? txnUnit,
            string remark)
        {
            return new ExchangeRate
            {
                Currency = currency,
                Date = date,
                Buying = buying,
                Selling = selling,
                TxnUnit = txnUnit,
                Remark = remark
            };
        }
    }
}
