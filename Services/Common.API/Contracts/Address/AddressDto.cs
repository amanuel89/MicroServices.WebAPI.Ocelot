using System.ComponentModel.DataAnnotations;

namespace RideBackend.API.Contracts.Addresses;
public class AddressDto
{
    [Required(ErrorMessage = "Address Name is mandatory.")]
    public string AddressName { get; set; }
    [Required(ErrorMessage = "Address Type is mandatory.")]
    public AddressType AddressType { get; set; }
    public long? ParentID { get; set; }

  
}

