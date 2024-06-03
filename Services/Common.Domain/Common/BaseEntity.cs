using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Domain.Common
{
    public class BaseEntity
    {
        public long Id { get;  set; }
        public bool IsActive { get; private set; }
        [MaxLength(26)]
        public string TrxnUnit { get; private set; }
        [MaxLength(100)]
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
        public virtual void UpdateRecordStatus (bool status, string updateBy = "")
        {
            IsActive = status;
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
