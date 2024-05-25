using AutoMapper;
using RideBackend.Domain.Models;


namespace RideBackend.API.Contracts;
public class ProfileMap : Profile
{
    public ProfileMap()
    {
     
        CreateMap<Address, AddressDetailDto>().ReverseMap();
        CreateMap<Address, AddressesHierarchyDto>().ReverseMap();
    }
}
