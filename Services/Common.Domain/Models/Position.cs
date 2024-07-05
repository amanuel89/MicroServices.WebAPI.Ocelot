using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Position", Schema = "Common")]
    public class Position : BaseEntity
    { 
        [MaxLength(100)]
        public string Description { get; private set; } = string.Empty;
    }
}