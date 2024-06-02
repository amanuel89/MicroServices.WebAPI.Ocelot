using CommonService.Domain.Common;

namespace CommonService.Domain.Models
{

    public class Holiday : BaseEntity
    {
        public long? HolidayDefinitionId { get; private set; }
        public DateTime? ForecastedDate { get; private set; }
        public DateTime? ActualDate { get; private set; }
        public DateTime? CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public long? TrxnUnit { get; private set; }
        public string Remark { get; private set; } = string.Empty;

        public virtual HolidayDefinition HolidayDefinition { get; private set; }

        public static Holiday Create(
            long? holidayDefinitionId,
            DateTime? forecastedDate,
            DateTime? actualDate,
            DateTime? createdOn,
            DateTime? updatedOn,
            long? trxnUnit,
            string remark)
        {
            return new Holiday
            {
                HolidayDefinitionId = holidayDefinitionId,
                ForecastedDate = forecastedDate,
                ActualDate = actualDate,
                CreatedOn = createdOn,
                UpdatedOn = updatedOn,
                TrxnUnit = trxnUnit,
                Remark = remark
            };
        }
    }
}
