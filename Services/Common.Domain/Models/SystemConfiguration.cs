public class SystemConfiguration
{
    public int Id { get; set; }
    public string Attribute { get; set; }
    public string CurrentValue { get; set; }
    public string PreviousValue { get; set; }
    public int? TrxnUnit { get; set; }
    public string Remark { get; set; }

    // Factory methods
    public static SystemConfiguration Create(int id, string attribute)
    {
        return new SystemConfiguration
        {
            Id = id,
            Attribute = attribute
        };
    }

    public void Update(string attribute, string currentValue, string previousValue, int? trxnUnit, string remark)
    {
        Attribute = attribute;
        CurrentValue = currentValue;
        PreviousValue = previousValue;
        TrxnUnit = trxnUnit;
        Remark = remark;
    }
}
