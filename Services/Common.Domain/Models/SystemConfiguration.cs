using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Common.SystemConfiguration")]
public class SystemConfiguration : BaseEntity
{
    [MaxLength(100)]
    public string Attribute { get; set; }
    [MaxLength(200)]
    public string CurrentValue { get; set; }
    [MaxLength(200)]
    public string PreviousValue { get; set; }

    // Factory methods
    public static SystemConfiguration Create(int id, string attribute)
    {
        return new SystemConfiguration
        {
            Id = id,
            Attribute = attribute
        };
    }

    public void Update(string attribute, string currentValue, string previousValue)
    {
        Attribute = attribute;
        CurrentValue = currentValue;
        PreviousValue = previousValue;
    }
}
