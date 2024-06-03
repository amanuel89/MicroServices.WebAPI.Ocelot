using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Common.OrganizationBankAccount")]
public class OrganizationBankAccount : BaseEntity
{
    public int? Organization { get; set; }
    public int? OrganizationUnit { get; set; }
    public int? PaymentProcessor { get; set; }
    public int? PaymentProcessorUnit { get; set; }
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    public int? Type { get; set; }
    public int? Category { get; set; }
    [MaxLength(50)]
    public string Credential { get; set; } = string.Empty;
    [MaxLength(100)]
    public string AccountNo { get; set; } = string.Empty;
    public bool? IsDigital { get; set; }

    // Factory methods
    public static OrganizationBankAccount Create(int id)
    {
        return new OrganizationBankAccount
        {
            Id = id
        };
    }

    public void Update(int? organization, int? organizationUnit, int? paymentProcessor, int? paymentProcessorUnit, string description, int? type, int? category, string credential, string accountNo, bool? isDigital)
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
    }
}
