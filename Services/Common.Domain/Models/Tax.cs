public class Tax
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int? Category { get; set; }
    public string Description { get; set; }
    public double? Amount { get; set; }
    public int? TrxnUnit { get; set; }
    public string Remark { get; set; }

    // Factory methods
    public static Tax Create(int id, string code)
    {
        return new Tax
        {
            Id = id,
            Code = code
        };
    }

    public void Update(string code, int? category, string description, double? amount, int? trxnUnit, string remark)
    {
        Code = code;
        Category = category;
        Description = description;
        Amount = amount;
        TrxnUnit = trxnUnit;
        Remark = remark;
    }
}
