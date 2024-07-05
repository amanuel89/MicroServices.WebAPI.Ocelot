using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("BaseObject", Schema = "Common")]
    public class BaseObject : BaseEntity
    {
        [MaxLength(50)]
        public string Description { get; private set; } = string.Empty;
        public bool? Deleted { get; private set; }      
    }
}
