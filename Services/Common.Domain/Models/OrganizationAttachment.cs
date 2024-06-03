using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Common.OrganizationAttachment")]
public class OrganizationAttachment : BaseEntity
{
    public int Type { get; set; }
    public int Category { get; set; }
    public int Index { get; set; }
    [MaxLength(200)]
    public string Url { get; set; }
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;



}
