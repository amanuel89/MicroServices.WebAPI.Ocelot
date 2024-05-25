using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideBackend.Domain.Common
{
    public class BaseEntity
    {
        public long Id { get;  set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; } = DateTime.MaxValue;
        public string TimeZoneInfo { get; private set; } = string.Empty;
        public DateTime RegisteredDate { get; private set; }
        public string RegisteredBy { get; private set; } = string.Empty;
        public DateTime LastUpdateDate { get; private set; }
        public string UpdatedBy { get; private set; } = string.Empty;
        public RecordStatus RecordStatus { get; private set; }
        public bool IsReadOnly { get;  set; }
        public string Remark { get; set; } = string.Empty;
        public string TrnxUnit { get; set; } = string.Empty;
        public BaseEntity()
        {
            StartDate = DateTime.UtcNow;
            EndDate = DateTime.MaxValue;
            RegisteredDate = DateTime.UtcNow;
            LastUpdateDate = DateTime.UtcNow;
            IsReadOnly = false;
            RecordStatus = RecordStatus.Active;
        }

        public virtual void UpdateAudit(string updateBy = "")
        {
            LastUpdateDate = DateTime.UtcNow;
            UpdatedBy = updateBy;
        }
        public virtual void UpdateRecordStatus (RecordStatus status, string updateBy = "")
        {
            RecordStatus = status;
            LastUpdateDate = DateTime.UtcNow;
            UpdatedBy = updateBy;
        }
        public virtual void Register(string registredBy = "")
        {
            RegisteredDate = DateTime.UtcNow;
            StartDate = DateTime.UtcNow;
            RegisteredBy = registredBy;
        }
        public virtual void Delete(string deletedBy = "")
        {
            RecordStatus = RecordStatus.Deleted;
            LastUpdateDate = DateTime.UtcNow;
            UpdatedBy = deletedBy;
        }
     
    }
}
