using AutoMapper;
using Microsoft.AspNetCore.Http;
using RideBackend.Application.Services.Helper;
using System.Text.RegularExpressions;

namespace RideBackend.Application.Commands;
public class CreateDriver : IRequest<OperationResult<DriverResponseDTO>>
{
    public DriverCreateRequestDTO Driver { get; set; }
}

public class CreateDriverHandler : IRequestHandler<CreateDriver, OperationResult<DriverResponseDTO>>
{
    private readonly IRepositoryBase<Driver> _driverRepository;
    private readonly IRepositoryBase<VehicleTypes> _VehicleTypes;
    private readonly IMapper _mapper;
    private readonly ImageUploader _imageUploader;
    public CreateDriverHandler(IRepositoryBase<Driver> driverRepository, IRepositoryBase<VehicleTypes> VehicleTypesRepository, IMapper mapper, ImageUploader imageUploader)
    {
        _driverRepository = driverRepository;
        _VehicleTypes = VehicleTypesRepository;
        _mapper = mapper;
        _imageUploader = imageUploader;
    }

    public async Task<OperationResult<DriverResponseDTO>> Handle(CreateDriver request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<DriverResponseDTO>();
        var vehicleRequest = request.Driver.Vehicle;
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
        try
        {
            if (request == null || vehicleRequest == null)
            {
                result.AddError(ErrorCode.ValidationError, "Request is not complete.");
                return result;
            }
            if (!ValidatePhoneNumber(request.Driver.PhoneNumber))
            {
                result.AddError(ErrorCode.ValidationError, "Phone number is not valid.");
                return result;
            }
            if (_driverRepository.ExistWhere(x => (x.Email == request.Driver.Email || x.PhoneNumber == request.Driver.PhoneNumber) && x.RecordStatus != RecordStatus.Deleted))
            {
                result.AddError(ErrorCode.RecordAlreadyExists, "Driver with this email or phone number is already registered.");
                return result;
            }

            if (_driverRepository.ExistWhere(x => x.DriverUserName == request.Driver.PhoneNumber && x.RecordStatus != RecordStatus.Deleted))
            {
                result.AddError(ErrorCode.RecordAlreadyExists, "User is already Registered.");
                return result;
            }

            if (!_VehicleTypes.ExistWhere(x => x.Id == vehicleRequest.VehicleTypeId && x.RecordStatus == RecordStatus.Active))
            {
                result.AddError(ErrorCode.ValidationError, "Vehicle Type Not Found.");
                return result;
            }

            var driverImagePath = "";
            var drivingLicense = "";
            //upload driver photo 
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
                        result.AddError(ErrorCode.ValidationError, "Driver photo upload has issue: " + driverImage.ErrorMessage);
                        return result;
                    }
                    driverImagePath = driverImage.Path;
                }
            }
            //upload license //to-be refactored with driver photo
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
                        result.AddError(ErrorCode.ValidationError, "License photo upload has issue: " + license.ErrorMessage);
                        return result;
                    }
                    drivingLicense = license.Path;
                }
            }
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
                            imagePaths[property.Name]=uploadResponse.Path;
                        }
                    }
                }
            }

            var driver = Driver.Create(request.Driver.PhoneNumber, request.Driver.FirstName, request.Driver.MiddleName, request.Driver.LastName,
                                        drivingLicense, request.Driver.PhoneNumber, request.Driver.TradeLicenseAndTin, driverImagePath,
                                        request.Driver.Gender, request.Driver.Email);


            var vehicle = Vehicle.Create(vehicleRequest.VehicleMaker, vehicleRequest.Model, vehicleRequest.ManufacturingYear,
                                        vehicleRequest.VehicleTypeId, vehicleRequest.PlateNumber, vehicleRequest.Color, imagePaths["SideOne"],
                                        imagePaths["SideTwo"], imagePaths["Front"], imagePaths["Rear"], imagePaths["Insurance"],
                                        imagePaths["PowerOfAttorney"], vehicleRequest.IsOwner, imagePaths["Libre"]);

            driver.Vehicle.Add(vehicle);
            await _driverRepository.AddAsync(driver);
            var response = _mapper.Map<DriverResponseDTO>(driver);
            result.Payload = response;
            result.Message = "Operation success";
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError, $"Something went wrong try again letter.");
        }

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


    public static bool ValidatePhoneNumber(string phoneNumber)
    {
        string pattern = @"^(\+251[79]\s)?(0?[79]|\d{1})\s?\d{8}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
    }
