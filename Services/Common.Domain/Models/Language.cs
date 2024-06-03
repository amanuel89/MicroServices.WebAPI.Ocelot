using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Common.Languge")]
    public class Languge : BaseEntity
    {
        [MaxLength(100)]
        public string LangugeName { get; set; } = string.Empty;
        [MaxLength(2)]
        public string LangugeShortCode { get; set; } = string.Empty;
        [MaxLength(200)]
        public string LangugeDescription { get; set; } = string.Empty;
        public bool? IsDefault { get; set; }

        public static Languge Create(int id, string langugeName, string langugeShortCode, string langugeDescription , bool? isDefault, string remark)
        {
            return new Languge
            {
                Id = id,
                LangugeName = langugeName,
                LangugeShortCode = langugeShortCode,
                LangugeDescription = langugeDescription,
                IsDefault = isDefault,
            };
        }
    }

}
