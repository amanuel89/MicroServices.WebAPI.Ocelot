using AutoMapper;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;

public class UpdatePassenger : IRequest<OperationResult<PassengerResponseDTO>>
{
    public long Id { get; set; }
    public required PassengerUpdateDTO Passenger { get; set; }
}
public class EditPassengerHandler : IRequestHandler<UpdatePassenger, OperationResult<PassengerResponseDTO>>
{
    private readonly IRepositoryBase<Passenger> _Passenger;
    private readonly IMapper _mapper;
    private readonly ImageUploader _imageUploader;
    public EditPassengerHandler(IRepositoryBase<Passenger> _Passenger, IMapper imapper,ImageUploader imageUploader)
    {
        this._Passenger = _Passenger;
        this._mapper = imapper;
        _imageUploader = imageUploader;
    }
    public async Task<OperationResult<PassengerResponseDTO>> Handle(UpdatePassenger request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<PassengerResponseDTO>();
        var passenger = _Passenger.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);

        if (passenger == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }

        var req = request.Passenger;
        //if (_Passenger.ExistWhere(x => (x.Email == req.Email || x.Phone==req.Phone) && x.Id!=request.Id))
        //{
        //    result.AddError(ErrorCode.RecordAlreadyExists, "Passenger with this email or phone number is already exists.");
        //    return result;
        //}

        var passengerPhoto = passenger.ProfilePictureUrl;
        if (req.ProfilePictureUrl != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await req.ProfilePictureUrl.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string fileName = $"{req.Name}_{DateTime.Now:yyyyMMddHHmmss}";
                ImageUploadResponse image = _imageUploader.UploadToWwwRoot(fileBytes, fileName, IMAGECATEGORY.DRIVERSPHOTO);

                if (!image.Success)
                {
                    result.AddError(ErrorCode.ValidationError, "Photo upload has issue: " + image.ErrorMessage);
                    return result;
                }
                passengerPhoto = image.Path;
            }
        }

        passenger.Update(req.Name,req.Email,passengerPhoto);
        _Passenger.Update(passenger);
        var response = _mapper.Map<PassengerResponseDTO>(passenger);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}
