using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Common.ObjectType")]
public class ObjectType : BaseEntity
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(26)]
    public string Abbriviation { get; set; } = string.Empty;
    public string BaseObject { get; set; } = string.Empty;
    [MaxLength(100)]
    public string SubSystem { get; set; } = string.Empty;
    public bool IsLineItemObject { get; set; }
    public bool IsJournalObject { get; set; }
    public string JournalType { get; set; } = string.Empty;
    public string JournalAlgorithm { get; set; } = string.Empty;
    public string Parent { get; set; } = string.Empty;
    public int? Sequence { get; set; }
    public bool Deleted { get; set; }

    // Factory methods
    public static ObjectType Create(int id, string description, string baseObject)
    {
        return new ObjectType
        {
            Id = id,
            Description = description,
            BaseObject = baseObject,
            Deleted = false
        };
    }

    public void Update(string description, string abbriviation, string baseObject, string subSystem, bool isLineItemObject, bool isJournalObject, string journalType, string journalAlgorithm, bool isActive, string parent, int? sequence)
    {
        Description = description;
        Abbriviation = abbriviation;
        BaseObject = baseObject;
        SubSystem = subSystem;
        IsLineItemObject = isLineItemObject;
        IsJournalObject = isJournalObject;
        JournalType = journalType;
        JournalAlgorithm = journalAlgorithm;
        Parent = parent;
        Sequence = sequence;
    }
}
