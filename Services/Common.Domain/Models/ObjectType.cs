using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ObjectType", Schema = "Common")]
public class ObjectType : BaseEntity
{
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(26)]
    public string Abbriviation { get; set; } = string.Empty;
    public long BaseObjectId { get; set; } 
    [MaxLength(100)]
    public int SubSystemId { get; set; } //systemlookup
    public bool IsLineItemObject { get; set; }
    public bool IsJournalObject { get; set; }
    public int JournalTypeId { get; set; } //systemlookup
    public int JournalAlgorithmId { get; set; } //systemlookup
    public long Parent { get; set; }
    public int? Index { get; set; }
    public virtual SystemLookup SubSystem { get; set; }
    public virtual SystemLookup JournalType { get; set; }
    public virtual SystemLookup JournalAlgorithm { get; set; }
    public static ObjectType Create(int id, string description, string baseObject)
    {
        return new ObjectType
        {
            Id = id,
            Description = description
        };
    }

  
}
