public class Subscription
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public int? TrxnUnit { get; set; }
    public string Remark { get; set; }

    // Factory methods
    public static Subscription Create(int id, string code, string name)
    {
        return new Subscription
        {
            Id = id,
            Code = code,
            Name = name
        };
    }

    public void Update(string code, string name, string description, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? trxnUnit, string remark)
    {
        Code = code;
        Name = name;
        Description = description;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        UpdatedOn = updatedOn;
        UpdatedBy = updatedBy;
        TrxnUnit = trxnUnit;
        Remark = remark;
    }
}
