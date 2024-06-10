using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Common.OrganizationUnit")]
public class OrganizationUnit : BaseEntity
{
    [MaxLength(26)]
    public string Code { get; set; } = string.Empty;
    public long? OrganizationId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    public int? Type { get; set; } //systemlookup

    public int? Specialization { get; set; }//systemlookup

    public int? Purpose { get; set; }//systemlookup

    public long? CategoryId { get; set; }

    [MaxLength(20)]
    public string Abbreviation { get; set; } = string.Empty;

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    [MaxLength(100)]
    public string Contact { get; set; }

    [MaxLength(50)]
    public string Phone1 { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Phone2 { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Website { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string SpecificAddress { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Street { get; set; } = string.Empty;

    [MaxLength(26)]
    public string HouseNumber { get; set; } = string.Empty;

    [MaxLength(26)]
    public string PoBox { get; set; } = string.Empty;

    [MaxLength(26)]
    public string Kebele { get; set; } = string.Empty;

    [MaxLength(26)]
    public string Wereda { get; set; } = string.Empty;

    public int? Subcity { get; set; } //systemlookup

    public int? City { get; set; }//systemlookup

    public int? Region { get; set; } //systemlookup

    public int? CountryId { get; set; }

    [MaxLength(100)]
    public string Address_I { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Address_II { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Address_III { get; set; } = string.Empty;

    public int? ParentId { get; set; }

    public int? DefaultTag { get; set; } // tag id

    public bool? IsEcommerce { get; set; }

    [MaxLength(200)]
    public string ImageUrl { get; set; } = string.Empty;

    // Factory methods
    public static OrganizationUnit Create(int id, string name)
    {
        return new OrganizationUnit
        {
            Id = id,
            Name = name
        };
    }

    public void Update(string code, int? organization, string name, string description, int? type, int? specialization, int? purpose, int? category, string abbreviation, double? longitude, double? latitude, string contact, string phone1, string phone2, string email, string website, string specificAddress, string street, string houseNumber, string poBox, string kebele, string wereda, int? subcity, int? city, int? region, int? country, string address_I, string address_II, string address_III, int? parentId, int? defaultTag, bool? isEcommerce, string imageUrl)
    {
        Code = code;
        OrganizationId = organization;
        Name = name;
        Description = description;
        Type = type;
        Specialization = specialization;
        Purpose = purpose;
        CategoryId = category;
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
        CountryId = country;
        Address_I = address_I;
        Address_II = address_II;
        Address_III = address_III;
        ParentId = parentId;
        DefaultTag = defaultTag;
        IsEcommerce = isEcommerce;
        ImageUrl = imageUrl;
    }
}
