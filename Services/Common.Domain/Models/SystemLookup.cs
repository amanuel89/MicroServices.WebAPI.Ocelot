using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("SystemLookup", Schema = "Common")]
public class SystemLookup: BaseEntity
{
    public int? Index { get; set; }
    public bool? IsSystemDefined { get; set; }
    [MaxLength(26)]
    public string Type { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    [MaxLength(200)]
    public string Value { get; set; }
    public bool? IsDefault { get; set; }

    // Factory methods
    public static SystemLookup Create(int? index, bool? isSystemDefined, string type, string description, string value, bool? isDefault)
    {
        return new SystemLookup
        {
           Index = index,
           IsSystemDefined = isSystemDefined,
           Type = type,
           Description = description,
           Value = value,
           IsDefault = isDefault
        };
    }

    public void Update(int? index, bool? isSystemDefined, string type, string description, string value, bool? isDefault)
    {
        Index = index;
        IsSystemDefined = isSystemDefined;
        Type = type;
        Description = description;
        Value = value;
        IsDefault = isDefault;
    }
}
