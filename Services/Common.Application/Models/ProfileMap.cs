using AutoMapper;

namespace Common.Application.Models;
public class ProfileMap : Profile
{
    public ProfileMap()
    {  
        CreateMap<Country, CountryCreateRequest>().ReverseMap();
        CreateMap<Country, CountryListResponse>().ReverseMap();
        CreateMap<Country, CountrySingleResponse>().ReverseMap();
    }
}
