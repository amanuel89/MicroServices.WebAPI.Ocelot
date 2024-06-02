public class TagDefinition
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsSystemDefined { get; set; }
    public int? TrxnUnit { get; set; }
    public string Remark { get; set; }

    // Factory methods
    public static TagDefinition Create(int id, string description)
    {
        return new TagDefinition
        {
            Id = id,
            Description = description
        };
    }

    public void Update(string description, bool? isActive, bool? isSystemDefined, int? trxnUnit, string remark)
    {
        Description = description;
        IsActive = isActive;
        IsSystemDefined = isSystemDefined;
        TrxnUnit = trxnUnit;
        Remark = remark;
    }
}
