using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("FMS.AccountCategory")]
    public class Languge : BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string LangugeName { get; set; }
        [MaxLength(2)]
        public string LangugeShortCode { get; set; }
        [MaxLength(200)]
        public string LangugeDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDefault { get; set; }
        [MaxLength(100)]
        public string Remark { get; set; }

        public static Languge Create(int id, string langugeName, string langugeShortCode, string langugeDescription, bool? isActive, bool? isDefault, string remark)
        {
            return new Languge
            {
                Id = id,
                LangugeName = langugeName,
                LangugeShortCode = langugeShortCode,
                LangugeDescription = langugeDescription,
                IsActive = isActive,
                IsDefault = isDefault,
                Remark = remark
            };
        }
    }

}
