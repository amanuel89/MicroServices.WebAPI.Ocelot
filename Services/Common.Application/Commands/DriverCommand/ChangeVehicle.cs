using AutoMapper;
using Microsoft.AspNetCore.Http;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;

public class ChangeVehicle : IRequest<OperationResult<DriverResponseDTO>>
{
    public long Id { get; set; }
    public required VehicleCreateRequest Vehicle { get; set; }
}
public class ChangeVehicleHandler : IRequestHandler<ChangeVehicle, OperationResult<DriverResponseDTO>>
{
    private readonly IRepositoryBase<Driver> _Driver;
    private readonly IRepositoryBase<Vehicle> _Vehicle;
    private readonly IMapper _mapper;
    private readonly ImageUploader _imageUploader;
    public ChangeVehicleHandler(IRepositoryBase<Driver> _Driver, IRepositoryBase<Vehicle> _Vehicle, IMapper imapper,ImageUploader imageUploader)
    {
        this._Driver = _Driver;
        this._Vehicle=_Vehicle;
        this._mapper = imapper;
        _imageUploader = imageUploader;
    }
    public async Task<OperationResult<DriverResponseDTO>> Handle(ChangeVehicle request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<DriverResponseDTO>();
        Dictionary<string, string> imagePaths = new Dictionary<string, string>
                            {
                                { "SideOne", "" },
                                { "SideTwo", "" },
                                { "Front", "" },
                                { "Rear", "" },
                                { "PowerOfAttorney", "" },
                                { "Insurance", "" },
                                { "Libre", "" }
                            };
        if (request.Vehicle == null)
        {
            result.AddError(ErrorCode.NotFound, "Payload cannot be empty.");
            return result;
        }
        var Driver = _Driver.Where(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted).Include(x=>x.Vehicle).FirstOrDefault();

        if (Driver == null)
        {
            result.AddError(ErrorCode.NotFound, "Driver doesn't exist.");
            return result;
        }

        var vehicleRequest = request.Vehicle;

        foreach (var property in typeof(VehicleCreateRequest).GetProperties())
        {
            if (property.PropertyType == typeof(IFormFile))
            {
                var file = (IFormFile)property.GetValue(vehicleRequest);

                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();
                        string fileName = $"{Guid.NewGuid()}_{file.FileName}";
                        ImageUploadResponse uploadResponse = _imageUploader.UploadToWwwRoot(fileBytes, fileName, GetImageCategory(property.Name));

                        if (!uploadResponse.Success)
                        {
                            result.AddError(ErrorCode.ValidationError, $"Failed to upload {property.Name} file. Error: {uploadResponse.ErrorMessage}");
                            return result;
                        }
                        imagePaths[property.Name] = uploadResponse.Path;
                    }
                }
            }
        }
        var vehicle = Vehicle.Create(vehicleRequest.VehicleMaker, vehicleRequest.Model, vehicleRequest.ManufacturingYear,
                                     vehicleRequest.VehicleTypeId, vehicleRequest.PlateNumber, vehicleRequest.Color, imagePaths["SideOne"],
                                     imagePaths["SideTwo"], imagePaths["Front"], imagePaths["Rear"], imagePaths["Insurance"],
                                     imagePaths["PowerOfAttorney"], vehicleRequest.IsOwner, imagePaths["Libre"]);
       

        foreach (var item in Driver.Vehicle)
        {
            item.UpdateRecordStatus(RecordStatus.InActive);
        }

        Driver.Vehicle.Add(vehicle);
        _Driver.Update(Driver);
        var response = _mapper.Map<DriverResponseDTO>(Driver);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
    private IMAGECATEGORY GetImageCategory(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(VehicleCreateRequest.SideOne):
            case nameof(VehicleCreateRequest.SideTwo):
            case nameof(VehicleCreateRequest.Front):
            case nameof(VehicleCreateRequest.Rear):
                return IMAGECATEGORY.VEHICLES;
            case nameof(VehicleCreateRequest.PowerOfAttorney):
                return IMAGECATEGORY.DOCUMENTS;
            case nameof(VehicleCreateRequest.Insurance):
                return IMAGECATEGORY.INSURANCE;
            case nameof(VehicleCreateRequest.Libre):
                return IMAGECATEGORY.LIBRE;
            default:
                return IMAGECATEGORY.OTHER;
        }
    }

}
