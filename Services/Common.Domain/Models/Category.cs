using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Common.Category")]
    public class Category : BaseEntity
    {
        [MaxLength(50)]
        public string Description { get; private set; } = string.Empty;
        public int Image { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsSystemDefined { get; private set; }
        public long ObjectTypeId { get; private set; }
        public long Parent { get; private set; }
    }
}
