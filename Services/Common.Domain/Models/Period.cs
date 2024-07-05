using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Period", Schema = "Common")]
    public class Period : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; private set; } = string.Empty;
        public long TypeId { get; private set; } //syslookupid
        public long CategoryId { get; private set; }//syslookupid
        public DateTime BeginningDate { get; private set; } 
        public DateTime EndingDate { get; private set; }
        public int Parent { get; private set; } 
        public int Index { get; private set; }
        public virtual SystemLookup Type { get; set; }
        public virtual SystemLookup Category { get; set; }
    }
}