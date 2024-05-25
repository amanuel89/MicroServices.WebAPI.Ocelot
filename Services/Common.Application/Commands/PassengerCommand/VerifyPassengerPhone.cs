using RideBackend.Application.Services.Helper;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands.PassengerCommand;

public class VerifyPassengerPhone : IRequest<OperationResult<bool>>
{
    public long Id { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
}
public class VerifyPassengerPhoneHandler : IRequestHandler<VerifyPassengerPhone, OperationResult<bool>>
{
    private readonly IRepositoryBase<Passenger> _Passenger;
    private readonly IRepositoryBase<VerificationCodes> _VerificationCodes;
    public VerifyPassengerPhoneHandler(IRepositoryBase<Passenger> _Passenger, IRepositoryBase<VerificationCodes> _VerificationCodes)
    {
        this._Passenger = _Passenger;
        this._VerificationCodes = _VerificationCodes;
    }
    public async Task<OperationResult<bool>> Handle(VerifyPassengerPhone request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        var Passenger = _Passenger.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
        if (Passenger == null)
        {
            result.IsError = true;
            result.Payload = false;
            result.Message = "Unable to verify";
            return result;
        }
        var normalizedPhone = HelperUtil.NormalizePhoneNumber(request.Phone);
        var verification = _VerificationCodes.FirstOrDefault(x => x.VerifierId == request.Id && x.VerificationCode == request.Code && x.Phone == normalizedPhone && x.RecordStatus == RecordStatus.InActive);
        if (verification != null)
        {
            Passenger.Verify(true);
            verification.Delete();

            _Passenger.Update(Passenger);
            _VerificationCodes.Update(verification);

            result.IsError = false;
            result.Payload = true;
            result.Message = "Operation success";
            return result;
        }
        else
        {
            result.IsError = true;
            result.Payload = false;
            result.Message = "Unable to verify";
            return result;
        }
     

       
    }
}
