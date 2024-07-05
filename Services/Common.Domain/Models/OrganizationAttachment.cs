using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("OrganizationAttachment", Schema = "Common")]
public class OrganizationAttachment : BaseEntity
{
    public long TypeId { get; set; } //systemlookup
    public long CategoryId { get; set; }
    public int Index { get; set; }
    [MaxLength(200)]
    public string Url { get; set; }
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    public virtual Category Category { get; set; }
    public virtual SystemLookup Type { get; set; }


}
