using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Common.TagDefinition")]
public class TagDefinition : BaseEntity
{
    [MaxLength(100)]
    public string Description { get; set; }
    public bool? IsSystemDefined { get; set; }

    // Factory methods
    public static TagDefinition Create(int id, string description)
    {
        return new TagDefinition
        {
            Id = id,
            Description = description
        };
    }

    public void Update(string description, bool? isSystemDefined)
    {
        Description = description;
        IsSystemDefined = isSystemDefined;
    }
}
