using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("ExchangeRate",Schema = "Common")]
    public class ExchangeRate : BaseEntity
    {
        public long? CurrencyId { get; private set; }
        public DateTime? Date { get; private set; }
        public decimal? Buying { get; private set; }
        public decimal? Selling { get; private set; }
        public virtual Currency Currency { get; private set; }
        public static ExchangeRate Create(
            long? currency,
            DateTime? date,
            decimal? buying,
            decimal? selling)
        {
            return new ExchangeRate
            {
                CurrencyId = currency,
                Date = date,
                Buying = buying,
                Selling = selling
            };
        }
    
    public void Update(
            long? currency,
            DateTime? date,
            decimal? buying,
            decimal? selling)
    {

            CurrencyId = currency;
            Date = date;
            Buying = buying;
            Selling = selling;
        
    }
}
}
