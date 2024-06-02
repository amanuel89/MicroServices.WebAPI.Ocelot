public class SystemLookup
{
    public int Id { get; set; }
    public int? Index { get; set; }
    public bool? IsSystemDefined { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
    public bool? IsDefault { get; set; }
    public bool? IsActive { get; set; }
    public int? TrxnUnit { get; set; }
    public string Remark { get; set; }

    // Factory methods
    public static SystemLookup Create(int id)
    {
        return new SystemLookup
        {
            Id = id
        };
    }

    public void Update(int? index, bool? isSystemDefined, string type, string description, string value, bool? isDefault, bool? isActive, int? trxnUnit, string remark)
    {
        Index = index;
        IsSystemDefined = isSystemDefined;
        Type = type;
        Description = description;
        Value = value;
        IsDefault = isDefault;
        IsActive = isActive;
        TrxnUnit = trxnUnit;
        Remark = remark;
    }
}
