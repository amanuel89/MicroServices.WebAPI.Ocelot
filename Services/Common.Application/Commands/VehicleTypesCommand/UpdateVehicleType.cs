using AutoMapper;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;

public class UpdateVehicleTypes : IRequest<OperationResult<VehicleTypesDTo>>
{
    public long Id { get; set; }
    public VehicleTypesUpdateDto VehicleTypes { get; set; }
}
public class EditVehicleTypesHandler : IRequestHandler<UpdateVehicleTypes, OperationResult<VehicleTypesDTo>>
{
    private readonly IRepositoryBase<VehicleTypes> _VehicleTypes;
    private readonly IMapper _mapper;
    private readonly ImageUploader _imageUploader;
    public EditVehicleTypesHandler(IRepositoryBase<VehicleTypes> _VehicleTypes, IMapper mapper, ImageUploader imageUploader)
    {

        this._VehicleTypes = _VehicleTypes;
        _mapper = mapper;
        _imageUploader = imageUploader;
    }
    public async Task<OperationResult<VehicleTypesDTo>> Handle(UpdateVehicleTypes request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<VehicleTypesDTo>();
        var VehicleTypes = _VehicleTypes.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);

        if (VehicleTypes == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        if (_VehicleTypes.ExistWhere(x => x.Name == request.VehicleTypes.Name && x.Id!=request.Id))
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Record already for same vehicle type name exists.");
            return result;
        }

        var vehicleTypeImageUrl = "";
        if (request.VehicleTypes.Image != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.VehicleTypes.Image.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string fileName = $"{request.VehicleTypes.Name}_{DateTime.Now:yyyyMMddHHmmss}";
                ImageUploadResponse image = _imageUploader.UploadToWwwRoot(fileBytes, fileName, IMAGECATEGORY.DRIVERSPHOTO);

                if (!image.Success)
                {
                    result.AddError(ErrorCode.ValidationError, "Photo upload has issue: " + image.ErrorMessage);
                    return result;
                }
                vehicleTypeImageUrl = image.Path;
            }
        }
        var req = request.VehicleTypes;
        VehicleTypes.Update(req.Name, vehicleTypeImageUrl, req.NumberOfPassengers, req.RatePerKm, req.InitialRate);
        _VehicleTypes.Update(VehicleTypes);
       
        var response = _mapper.Map<VehicleTypesDTo>(VehicleTypes);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}
