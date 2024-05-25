using AutoMapper;
using RideBackend.Application.Services.Helper;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;
public class CreatePassenger : IRequest<OperationResult<PassengerResponseDTO>>
{
    public PassengerRequestDTO Passenger { get;  set; }
}
public class CreatePassengerHandler : IRequestHandler<CreatePassenger, OperationResult<PassengerResponseDTO>>
{
    private readonly IRepositoryBase<Passenger> _Passenger;
    private readonly IMapper  _mapper;
    private readonly ImageUploader _imageUploader;
    private readonly IRepositoryBase<VerificationCodes> _VerificationCodes;
    public CreatePassengerHandler(IRepositoryBase<Passenger> _Passenger,IMapper imapper, ImageUploader imageUploader, IRepositoryBase<VerificationCodes> verificationCodes)
    {
        this._Passenger = _Passenger;
        _mapper = imapper;
        _imageUploader = imageUploader;
        _VerificationCodes = verificationCodes;
    }

    public async Task<OperationResult<PassengerResponseDTO>> Handle(CreatePassenger request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<PassengerResponseDTO>();

        try
        {
            if (_Passenger.ExistWhere(x => (x.Email == request.Passenger.Email || x.Phone == request.Passenger.Phone) && x.RecordStatus != RecordStatus.Deleted))
            {
                result.AddError(ErrorCode.RecordAlreadyExists, "Passenger with this email or phone number is already registered.");
                return result;
            }

            var req = request.Passenger;
            var passengerPhoto = "";
            if (req.ProfilePictureUrl != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await req.ProfilePictureUrl.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    string fileName = $"{req.Name}_{req.Phone}_{DateTime.Now:yyyyMMddHHmmss}";
                    ImageUploadResponse image = _imageUploader.UploadToWwwRoot(fileBytes, fileName, IMAGECATEGORY.DRIVERSPHOTO);

                    if (!image.Success)
                    {
                        result.AddError(ErrorCode.ValidationError, "Photo upload has issue: " + image.ErrorMessage);
                        return result;
                    }
                    passengerPhoto = image.Path;
                }
            }     

            var passenger = Passenger.Create(req.Name, req.Email, req.Password, req.Phone, passengerPhoto);
            await _Passenger.AddAsync(passenger);

            var twilioSmsVerification = new TwilioSmsVerification();
            var verificationCode = HelperUtil.GenerateVerificationCode().ToString();
            bool isVerificationCodeSent = await twilioSmsVerification.SendVerificationCodeAsync(HelperUtil.NormalizePhoneNumber(passenger.Phone), verificationCode);

            if (isVerificationCodeSent)
            {
                var referal = VerificationCodes.Create(HelperUtil.NormalizePhoneNumber(passenger.Phone), verificationCode, passenger.Id);
                await _VerificationCodes.AddAsync(referal);
            }

            var response = _mapper.Map<PassengerResponseDTO>(passenger);
            result.Payload = response;
            result.Message = "Operation success";
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError, $"error occurred passenger not registered.");
        }

        return result;
    }
}
