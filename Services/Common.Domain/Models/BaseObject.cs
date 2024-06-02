using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("FMS.AccountCategory")]
    public class BaseObject : BaseEntity
    {
        public string Description { get; private set; } = string.Empty;
        public bool Active { get; private set; }
        public bool? Deleted { get; private set; }
        public string TrxnUnit { get; private set; }
        public string Remark { get; private set; } = string.Empty;

       
    }
}
