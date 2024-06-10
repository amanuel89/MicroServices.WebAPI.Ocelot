using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Common.HolidayDefinition")]
    public class HolidayDefinition : BaseEntity
    {
        [MaxLength(150)]
        public string Description { get; private set; } = string.Empty;
        public long? Type { get; private set; } //syslookup
        public bool? IsFixed { get; private set; }
        public bool? WillClose { get; private set; }
        public int? AlertBefore { get; private set; }

        public virtual List<Holiday> Holidays { get; private set; } = new List<Holiday>();

        public static HolidayDefinition Create(
            string description,
            long? type,
            bool? isFixed,
            bool? willClose,
            int? alertBefore)
        {
            return new HolidayDefinition
            {
                Description = description,
                Type = type,
                IsFixed = isFixed,
                WillClose = willClose,
                AlertBefore = alertBefore
            };
        }
    }

}
