using ConsigneeService.Domain.Common;

namespace ConsigneeService.Domain.Models
{
    public class Staff : BaseEntity
    {
        public string Uid { get; private set; }
        public StaffRoles Role { get; private set; }
        public StaffStatus Status { get; private set; }

        public static Staff Create(string uid, StaffRoles role, StaffStatus status)
        {
            return new Staff
            {
                Uid = uid,
                Role = role,
                Status = status
            };
        }
    }
}
