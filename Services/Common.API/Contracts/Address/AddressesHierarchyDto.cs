using System.Security.Claims;

namespace RideBackend.API.Contracts.Addresses;
public class AddressesHierarchyDto
{
    public  string key { get; set; } 
    public Address data { get; set; }
    public List<AddressesHierarchyDto> children { get; set; }


}
