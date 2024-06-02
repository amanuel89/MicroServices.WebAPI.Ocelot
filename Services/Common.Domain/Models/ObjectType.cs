public class ObjectType
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Abbriviation { get; set; }
    public string BaseObject { get; set; }
    public string SubSystem { get; set; }
    public bool IsLineItemObject { get; set; }
    public bool IsJournalObject { get; set; }
    public string JournalType { get; set; }
    public string JournalAlgorithm { get; set; }
    public bool IsActive { get; set; }
    public string Parent { get; set; }
    public int? Sequence { get; set; }
    public bool Deleted { get; set; }
    public string TrxnUnit { get; set; }
    public string Remark { get; set; }

    // Factory methods
    public static ObjectType Create(int id, string description, string baseObject)
    {
        return new ObjectType
        {
            Id = id,
            Description = description,
            BaseObject = baseObject,
            IsActive = true,
            Deleted = false
        };
    }

    public void Update(string description, string abbriviation, string baseObject, string subSystem, bool isLineItemObject, bool isJournalObject, string journalType, string journalAlgorithm, bool isActive, string parent, int? sequence, string trxnUnit, string remark)
    {
        Description = description;
        Abbriviation = abbriviation;
        BaseObject = baseObject;
        SubSystem = subSystem;
        IsLineItemObject = isLineItemObject;
        IsJournalObject = isJournalObject;
        JournalType = journalType;
        JournalAlgorithm = journalAlgorithm;
        IsActive = isActive;
        Parent = parent;
        Sequence = sequence;
        TrxnUnit = trxnUnit;
        Remark = remark;
    }
}
