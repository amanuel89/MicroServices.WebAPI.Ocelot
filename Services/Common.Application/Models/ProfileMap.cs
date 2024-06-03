using AutoMapper;

namespace Common.Application.Models;
public class ProfileMap : Profile
{
    public ProfileMap()
    {  
        CreateMap<Country, CountryCreateRequest>().ReverseMap();
    }
}
