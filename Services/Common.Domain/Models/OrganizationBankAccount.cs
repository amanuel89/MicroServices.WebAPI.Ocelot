using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

[Table("OrganizationBankAccount", Schema = "Common")]
public class OrganizationBankAccount : BaseEntity
{
    public long? OrganizationId { get; set; }
    public long? OrganizationUnitId { get; set; }
    public int? PaymentProcessor { get; set; }
    public int? PaymentProcessorUnit { get; set; }
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    public long? TypeId { get; set; }
    public long? CategoryId { get; set; }
    [MaxLength(50)]
    public string Credential { get; set; } = string.Empty;
    [MaxLength(100)]
    public string AccountNo { get; set; } = string.Empty;
    public bool? IsDigital { get; set; }
    public virtual SystemLookup Type { get; set; }
    public virtual Category Category { get; set; }
    public virtual Organization Organization { get; set; }
    public virtual OrganizationUnit OrganizationUnit { get; set; }
    public static OrganizationBankAccount Create(int id)
    {
        return new OrganizationBankAccount
        {
            Id = id
        };
    }

    public void Update(long? organization, long? organizationUnit, int? paymentProcessor, int? paymentProcessorUnit, string description, long? type, long? category, string credential, string accountNo, bool? isDigital)
    {
        OrganizationId = organization;
        OrganizationUnitId = organizationUnit;
        PaymentProcessor = paymentProcessor;
        PaymentProcessorUnit = paymentProcessorUnit;
        Description = description;
        TypeId = type;
        CategoryId = category;
        Credential = credential;
        AccountNo = accountNo;
        IsDigital = isDigital;
    }
}
