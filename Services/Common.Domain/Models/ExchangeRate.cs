using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Common.ExchangeRate")]
    public class ExchangeRate : BaseEntity
    {
        public long? Currency { get; private set; }
        public DateTime? Date { get; private set; }
        public decimal? Buying { get; private set; }
        public decimal? Selling { get; private set; }

        public static ExchangeRate Create(
            long? currency,
            DateTime? date,
            decimal? buying,
            decimal? selling)
        {
            return new ExchangeRate
            {
                Currency = currency,
                Date = date,
                Buying = buying,
                Selling = selling
            };
        }
    }
}
