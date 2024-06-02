public class OrganizationBankAccount
{
    public int Id { get; set; }
    public int? Organization { get; set; }
    public int? OrganizationUnit { get; set; }
    public int? PaymentProcessor { get; set; }
    public int? PaymentProcessorUnit { get; set; }
    public string Description { get; set; }
    public int? Type { get; set; }
    public int? Category { get; set; }
    public string Credential { get; set; }
    public string AccountNo { get; set; }
    public bool? IsDigital { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public int? TrxnUnit { get; set; }
    public string Remark { get; set; }

    // Factory methods
    public static OrganizationBankAccount Create(int id)
    {
        return new OrganizationBankAccount
        {
            Id = id
        };
    }

    public void Update(int? organization, int? organizationUnit, int? paymentProcessor, int? paymentProcessorUnit, string description, int? type, int? category, string credential, string accountNo, bool? isDigital, DateTime? createdOn, DateTime? lastModifiedOn, int? trxnUnit, string remark)
    {
        Organization = organization;
        OrganizationUnit = organizationUnit;
        PaymentProcessor = paymentProcessor;
        PaymentProcessorUnit = paymentProcessorUnit;
        Description = description;
        Type = type;
        Category = category;
        Credential = credential;
        AccountNo = accountNo;
        IsDigital = isDigital;
        CreatedOn = createdOn;
        LastModifiedOn = lastModifiedOn;
        TrxnUnit = trxnUnit;
        Remark = remark;
    }
}
