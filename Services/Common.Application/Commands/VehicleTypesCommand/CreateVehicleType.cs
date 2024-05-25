using AutoMapper;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;
public class CreateVehicleTypes : IRequest<OperationResult<VehicleTypesDTo>>
{
    public VehicleTypesCreateDto VehicleType { get;  set; }
}
public class CreateVehicleTypesHandler : IRequestHandler<CreateVehicleTypes, OperationResult<VehicleTypesDTo>>
{
    private readonly IRepositoryBase<VehicleTypes> _VehicleTypes;
    private readonly IMapper _mapper;
    private readonly ImageUploader _imageUploader;
    public CreateVehicleTypesHandler(IRepositoryBase<VehicleTypes> _VehicleTypes, IMapper mapper, ImageUploader imageUploader)
    {
        this._VehicleTypes = _VehicleTypes;
        _mapper = mapper;
        _imageUploader = imageUploader;
    }
    public async Task<OperationResult<VehicleTypesDTo>> Handle(CreateVehicleTypes request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<VehicleTypesDTo>();

        if (_VehicleTypes.ExistWhere(x => (x.Name == request.VehicleType.Name) && x.RecordStatus != RecordStatus.Deleted))
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Record already for same vehicle type name exists.");
            return result;
        }
        var req = request.VehicleType;
        var vehicleTypeImageUrl = "";
        if (req.Image != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await req.Image.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string fileName = $"{req.Name}_{DateTime.Now:yyyyMMddHHmmss}";
                ImageUploadResponse image = _imageUploader.UploadToWwwRoot(fileBytes, fileName, IMAGECATEGORY.DRIVERSPHOTO);

                if (!image.Success)
                {
                    result.AddError(ErrorCode.ValidationError, "Photo upload has issue: " + image.ErrorMessage);
                    return result;
                }
                vehicleTypeImageUrl = image.Path;
            }
        }

        var vehicleTypes = VehicleTypes.Create(req.Name, vehicleTypeImageUrl, req.NumberOfPassengers, req.RatePerKm, req.InitialRate);
        await _VehicleTypes.AddAsync(vehicleTypes);
        var response = _mapper.Map<VehicleTypesDTo>(vehicleTypes);

        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}
