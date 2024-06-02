public class OrganizationUnit
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int? Organization { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Type { get; set; }
    public int? Specialization { get; set; }
    public int? Purpose { get; set; }
    public int? Category { get; set; }
    public string Abbreviation { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string Contact { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string SpecificAddress { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string PoBox { get; set; }
    public string Kebele { get; set; }
    public string Wereda { get; set; }
    public int? Subcity { get; set; }
    public int? City { get; set; }
    public int? Region { get; set; }
    public int? Country { get; set; }
    public string Address_I { get; set; }
    public string Address_II { get; set; }
    public string Address_III { get; set; }
    public int? ParentId { get; set; }
    public int? DefaultTag { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool? IsEcommerce { get; set; }
    public bool? IsActive { get; set; }
    public string ImageUrl { get; set; }
    public int? TrxnUnit { get; set; }
    public string Remark { get; set; }

    // Factory methods
    public static OrganizationUnit Create(int id, string name)
    {
        return new OrganizationUnit
        {
            Id = id,
            Name = name
        };
    }

    public void Update(string code, int? organization, string name, string description, int? type, int? specialization, int? purpose, int? category, string abbreviation, double? longitude, double? latitude, string contact, string phone1, string phone2, string email, string website, string specificAddress, string street, string houseNumber, string poBox, string kebele, string wereda, int? subcity, int? city, int? region, int? country, string address_I, string address_II, string address_III, int? parentId, int? defaultTag, DateTime? createdOn, DateTime? modifiedOn, bool? isEcommerce, bool? isActive, string imageUrl, int? trxnUnit, string remark)
    {
        Code = code;
        Organization = organization;
        Name = name;
        Description = description;
        Type = type;
        Specialization = specialization;
        Purpose = purpose;
        Category = category;
        Abbreviation = abbreviation;
        Longitude = longitude;
        Latitude = latitude;
        Contact = contact;
        Phone1 = phone1;
        Phone2 = phone2;
        Email = email;
        Website = website;
        SpecificAddress = specificAddress;
        Street = street;
        HouseNumber = houseNumber;
        PoBox = poBox;
        Kebele = kebele;
        Wereda = wereda;
        Subcity = subcity;
        City = city;
        Region = region;
        Country = country;
        Address_I = address_I;
        Address_II = address_II;
        Address_III = address_III;
        ParentId = parentId;
        DefaultTag = defaultTag;
        CreatedOn = createdOn;
        ModifiedOn = modifiedOn;
        IsEcommerce = isEcommerce;
        IsActive = isActive;
        ImageUrl = imageUrl;
        TrxnUnit = trxnUnit;
        Remark = remark;
    }
}
