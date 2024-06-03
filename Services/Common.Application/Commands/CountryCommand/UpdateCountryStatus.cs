using Common.Application.Models.Common;

namespace Common.Application.Commands.CountryCommand;

public class UpdateCountryStatus : IRequest<OperationResult<bool>>
{
    public long Id { get; set; }
}
public class UpdateCountryStatusHandler : IRequestHandler<UpdateCountryStatus, OperationResult<bool>>
{
    private readonly IRepositoryBase<Country> _Country;
    public UpdateCountryStatusHandler(IRepositoryBase<Country> _Country) => this._Country = _Country;
    public async Task<OperationResult<bool>> Handle(UpdateCountryStatus request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        var Country = _Country.FirstOrDefault(x => x.Id == request.Id);
        if (Country == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        Country.UpdateRecordStatus(!Country.IsActive);   
        result.Payload = _Country.Update(Country);
        result.Message = Country.IsActive==true ? "The Record is Activated Successfully" : "The Record is Deactivated Successfully";
        return result;
    }
}
