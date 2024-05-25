//using MediatR;

//namespace RideBackend.Application.Queries;
//public class SearchAddress : IRequest<OperationResult<List<Address>>>
//{
//    public string searchKey { get; set; }
//}
//public class SearchAddressHandler : IRequestHandler<SearchAddress, OperationResult<List<Address>>>
//{
//    private readonly IRepositoryBase<Address> _Address;
//    public SearchAddressHandler(IRepositoryBase<Address> _Address) => this._Address = _Address;
//    public async Task<OperationResult<List<Address>>> Handle(SearchAddress request, CancellationToken cancellationToken)
//    {
//        var result = new OperationResult<List<Address>>();
//        var Address = _Address.Where(x =>
//        x.RecordStatus == RecordStatus.Active && x.AddressName.Contains(request.searchKey)).ToList();

//        if (Address.Count() == 0)
//        {
//            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
//            return result;
//        }
//        result.Payload = Address;
//        result.Message = "Operation success";
//        return result;
//    }
//}
