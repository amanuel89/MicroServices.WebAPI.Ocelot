using AutoMapper;
using RideBackend.Domain.Dtos;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;

public class UpdateTariff : IRequest<OperationResult<TariffResponseDto>>
{
    public long Id { get; set; }
    public TariffUpdateRequesDto TariffUpdateRequesDto { get; set; }
}
public class UpdateTariffHandler : IRequestHandler<UpdateTariff, OperationResult<TariffResponseDto>>
{
    private readonly IRepositoryBase<VehicleTypes> _VehicleTypes;
    private readonly IRepositoryBase<Tariff> _tariff;
    private readonly IMapper _mapper;
    private readonly ImageUploader _imageUploader;
    public UpdateTariffHandler(IRepositoryBase<VehicleTypes> _VehicleTypes, IMapper mapper, ImageUploader imageUploader, IRepositoryBase<Tariff> tariff)
    {

        this._VehicleTypes = _VehicleTypes;
        _mapper = mapper;
        _imageUploader = imageUploader;
        _tariff = tariff;
    }
    public async Task<OperationResult<TariffResponseDto>> Handle(UpdateTariff request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<TariffResponseDto>();
        var tariff = _tariff.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
        if (tariff == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        var req = request.TariffUpdateRequesDto;
        tariff.Update( req.CostPerKm, req.CostPerMinute, req.PickupCost, req.DropOffCost, req.CancelCost,
            req.NightCostPerKm, req.NightCostPerMinute, req.NightPickupCost, req.NightDropOffCost, req.NightCancelCost, req.DayStartsOn,
            req.DayEndsOn, req.NightStartsOn, req.NightEndsOn);
        _tariff.Update(tariff);

        var response = _mapper.Map<TariffResponseDto>(tariff);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}
