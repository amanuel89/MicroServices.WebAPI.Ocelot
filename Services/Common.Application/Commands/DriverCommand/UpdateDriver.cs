using AutoMapper;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;

public class UpdateDriver : IRequest<OperationResult<DriverResponseDTO>>
{
    public long Id { get; set; }
    public required DriverUpdateRequest Driver { get; set; }
}
public class EditDriverHandler : IRequestHandler<UpdateDriver, OperationResult<DriverResponseDTO>>
{
    private readonly IRepositoryBase<Driver> _Driver;
    private readonly IMapper _mapper;
    private readonly ImageUploader _imageUploader;
    public EditDriverHandler(IRepositoryBase<Driver> _Driver, IMapper imapper,ImageUploader imageUploader)
    {
        this._Driver = _Driver;
        this._mapper = imapper;
        _imageUploader = imageUploader;
    }
    public async Task<OperationResult<DriverResponseDTO>> Handle(UpdateDriver request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<DriverResponseDTO>();
        if (request.Driver == null)
        {
            result.AddError(ErrorCode.NotFound, "Update Payload cannot be empty.");
            return result;
        }
        var Driver = _Driver.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);

        if (Driver == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }

        var req = request.Driver;
        if (_Driver.ExistWhere(x => (x.Email == req.Email) && x.Id!=request.Id))
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Driver with this email already exists.");
            return result;
        }


        var driverImagePath = Driver.Photo;
        var drivingLicense = Driver.DrivingLicense;   
        if (request.Driver.Photo != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.Driver.Photo.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string fileName = $"{request.Driver.FirstName}_{request.Driver.MiddleName}_{DateTime.Now:yyyyMMddHHmmss}";
                ImageUploadResponse driverImage = _imageUploader.UploadToWwwRoot(fileBytes, fileName, IMAGECATEGORY.DRIVERSPHOTO);

                if (!driverImage.Success)
                {
                    result.AddError(ErrorCode.ValidationError, "Driver attachment upload has issue: " + driverImage.ErrorMessage);
                    return result;
                }
                driverImagePath = driverImage.Path;
            }
        }
        if (request.Driver.DrivingLicense != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.Driver.DrivingLicense.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string fileName = $"{"license_"}{request.Driver.FirstName}_{request.Driver.MiddleName}_{DateTime.Now:yyyyMMddHHmmss}";
                ImageUploadResponse license = _imageUploader.UploadToWwwRoot(fileBytes, fileName, IMAGECATEGORY.LICENSE);

                if (!license.Success)
                {
                    result.AddError(ErrorCode.ValidationError, "License attachment upload has issue: " + license.ErrorMessage);
                    return result;
                }
                drivingLicense = license.Path;
            }
        }

        Driver.Update(req.FirstName,req.MiddleName,req.LastName, drivingLicense, req.TradeLicenseAndTin,driverImagePath,req.Gender,req.Email);
        _Driver.Update(Driver);
        var response = _mapper.Map<DriverResponseDTO>(Driver);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}
