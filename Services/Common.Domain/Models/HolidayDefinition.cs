using CommonService.Domain.Common;

namespace CommonService.Domain.Models
{
    public class HolidayDefinition : BaseEntity
    {
        public string Description { get; private set; } = string.Empty;
        public long? Type { get; private set; }
        public bool? IsFixed { get; private set; }
        public bool? WillClose { get; private set; }
        public long? AlertBefore { get; private set; }
        public long? TrxnUnit { get; private set; }
        public string Remark { get; private set; } = string.Empty;

        public virtual List<Holiday> Holidays { get; private set; } = new List<Holiday>();

        public static HolidayDefinition Create(
            string description,
            long? type,
            bool? isFixed,
            bool? willClose,
            long? alertBefore,
            long? trxnUnit,
            string remark)
        {
            return new HolidayDefinition
            {
                Description = description,
                Type = type,
                IsFixed = isFixed,
                WillClose = willClose,
                AlertBefore = alertBefore,
                TrxnUnit = trxnUnit,
                Remark = remark
            };
        }
    }

}
