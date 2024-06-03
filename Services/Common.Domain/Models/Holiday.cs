using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Common.Holiday")]
    public class Holiday : BaseEntity
    {
        public long? HolidayDefinitionId { get; private set; }
        public DateTime? ForecastedDate { get; private set; }
        public DateTime? ActualDate { get; private set; }
        public DateTime? CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }

        public virtual HolidayDefinition HolidayDefinition { get; private set; }

        public static Holiday Create(
            long? holidayDefinitionId,
            DateTime? forecastedDate,
            DateTime? actualDate,
            DateTime? createdOn,
            DateTime? updatedOn)
        {
            return new Holiday
            {
                HolidayDefinitionId = holidayDefinitionId,
                ForecastedDate = forecastedDate,
                ActualDate = actualDate,
                CreatedOn = createdOn,
                UpdatedOn = updatedOn
            };
        }
    }
}
