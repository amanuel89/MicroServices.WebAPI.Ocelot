public class Organization : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string OrganizationCode { get; set; }
    public string Database { get; set; }
    public string Blob { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Logo { get; set; }
    public int? SubscriptionId { get; set; }
    public int? DefaultLanguage { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public int? TrxnUnit { get; set; }

    // Factory methods
    public static Organization Create(int id, string name)
    {
        return new Organization
        {
            Id = id,
            Name = name
        };
    }

    public void Update(string name, string organizationCode, string database, string blob, string email, string description, string logo, int? subscriptionId, int? defaultLanguage, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? trxnUnit )
    {
        Name = name;
        OrganizationCode = organizationCode;
        Database = database;
        Blob = blob;
        Email = email;
        Description = description;
        Logo = logo;
        SubscriptionId = subscriptionId;
        DefaultLanguage = defaultLanguage;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        UpdatedOn = updatedOn;
        UpdatedBy = updatedBy;
        TrxnUnit = trxnUnit;
    }
}
