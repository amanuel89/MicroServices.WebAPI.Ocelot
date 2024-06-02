using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Domain.Common
{
    public class BaseEntity
    {
        public long Id { get;  set; }
        public bool Active { get; private set; }
        public string TrxnUnit { get; private set; }
        public string Remark { get; private set; } = string.Empty;
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public string CreatedBy { get; private set; } = string.Empty;
        public DateTime LastUpdateOn { get; private set; } = DateTime.Now;
        public string LastUpdateBy { get; private set; } = string.Empty;

        

        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
            LastUpdateOn = DateTime.UtcNow;
        }

        public virtual void UpdateAudit(string updateBy = "")
        {
            LastUpdateOn = DateTime.UtcNow;
            LastUpdateBy = updateBy;
        }
        public virtual void UpdateRecordStatus (RecordStatus status, string updateBy = "")
        {
            LastUpdateOn = DateTime.UtcNow;
            LastUpdateBy = updateBy;
        }
        public virtual void Register(string registredBy = "")
        {
            CreatedDate = DateTime.UtcNow;
            CreatedBy = registredBy;
        }
        
    }
}
