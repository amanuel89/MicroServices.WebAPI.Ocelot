using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Common.Period")]
    public class Period : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; private set; } = string.Empty;
        public int Type { get; private set; } 
        public int Category { get; private set; }
        public DateTime BeginningDate { get; private set; } 
        public DateTime EndingDate { get; private set; }
        public int Parent { get; private set; } 
        public int Index { get; private set; } 

    }
}