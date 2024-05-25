using AutoMapper;

namespace TargetEvaluationService.API.Contracts;
public class ProfileMap : Profile
{
    public ProfileMap()
    {
        CreateMap<Passenger, PassengerResponseDTO>().ReverseMap();
        CreateMap<Driver, DriverResponseDTO>().ReverseMap();
        CreateMap<Vehicle, VehicleResponseDTO>().ReverseMap();
        CreateMap<VehicleTypes, VehicleTypesDTo>().ReverseMap();
        CreateMap<Vehicle, DriverVehicleResponse>().ReverseMap();
        CreateMap<Bank, BankResponseDTO>().ReverseMap();
        CreateMap<Ride, RideResponseDto>().ReverseMap();
        CreateMap<Ride, RideNotificationDto>().ReverseMap();
        CreateMap<Tariff, TariffResponseDto>().ReverseMap();
        CreateMap<RideSettings, SettingsResponseDto>().ReverseMap();
    }
}
