using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Organization", Schema = "Common")]
public class Organization : BaseEntity
{
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(26)]
    public string OrganizationCode { get; set; } = string.Empty;
    [MaxLength(100)]
    public string TradeName { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Database { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Blob { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(100)]
    public string City { get; set; } = string.Empty;
    [MaxLength(100)]
    public string SubCity { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Wereda { get; set; } = string.Empty;
    [MaxLength(100)]
    public string PhoneNumber { get; set; } = string.Empty;
    [MaxLength(100)]
    public string WebSite { get; set; } = string.Empty;
    [MaxLength(50)]
    public string POBox { get; set; } = string.Empty;
    [MaxLength(50)]
    public string SpecificLocation { get; set; } = string.Empty;
    [MaxLength(100)]
    public string TIN_No { get; set; } = string.Empty;
    [MaxLength(26)]
    public string VATRegistrationNo { get; set; } = string.Empty;
    [MaxLength(26)]
    public string CompanyTaxType { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(500)]
    public string LogoUrl { get; set; } = string.Empty;
    public long? SubscriptionId { get; set; }
    public long? DefaultLanguage { get; set; }
   
    // Factory methods
    public static Organization Create(int id, string name)
    {
        return new Organization
        {
            Id = id,
            Name = name
        };
    }

    public void Update(string name, string organizationCode, string database, string blob, string email, string description, string logo, int? subscriptionId, int? defaultLanguage )
    {
        Name = name;
        OrganizationCode = organizationCode;
        Database = database;
        Blob = blob;
        Email = email;
        Description = description;
        SubscriptionId = subscriptionId;
        DefaultLanguage = defaultLanguage;
     
    }
}
