using AutoMapper;
using RideBackend.Domain.Dtos;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;
public class CreateTariff : IRequest<OperationResult<TariffResponseDto>>
{
    public TariffRequesDto TariffRequesDto { get; set; }
}
public class CreateTariffHandler : IRequestHandler<CreateTariff, OperationResult<TariffResponseDto>>
{
    private readonly IRepositoryBase<VehicleTypes> _VehicleTypes;
    private readonly IRepositoryBase<Tariff> _Tariff;
    private readonly IMapper _mapper;
    public CreateTariffHandler(IRepositoryBase<VehicleTypes> _VehicleTypes, IMapper mapper, IRepositoryBase<Tariff> tariff)
    {
        this._VehicleTypes = _VehicleTypes;
        _mapper = mapper;
        _Tariff = tariff;
    }
    public async Task<OperationResult<TariffResponseDto>> Handle(CreateTariff request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<TariffResponseDto>();

        if (!_VehicleTypes.ExistWhere(x => (x.Id == request.TariffRequesDto.VehicleId) && x.RecordStatus != RecordStatus.Deleted))
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Vehicle Type Doesn't exist.");
            return result;
        }
        if (_Tariff.ExistWhere(x => (x.VehicleId == request.TariffRequesDto.VehicleId) && x.RecordStatus != RecordStatus.Deleted))
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "There is already an active tarrif for the vehicle type.");
            return result;
        }
        var req = request.TariffRequesDto;
        var tariff =Tariff.Create(req.VehicleId,req.CostPerKm,req.CostPerMinute,req.PickupCost,req.DropOffCost,req.CancelCost,
            req.NightCostPerKm,req.NightCostPerMinute,req.NightPickupCost,req.NightDropOffCost,req.NightCancelCost,req.DayStartsOn,
            req.DayEndsOn,req.NightStartsOn,req.NightEndsOn);
      
        await _Tariff.AddAsync(tariff);
        var response = _mapper.Map<TariffResponseDto>(tariff);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}
