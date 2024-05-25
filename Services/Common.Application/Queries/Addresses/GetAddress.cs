//using RideBackend.Domain.Models;

//namespace RideBackend.Application.Queries
//{
//    public class GetAddress : IRequest<OperationResult<Address>>
//    {
//        public long Id { get; set; }
//    }
//    public class GetAddressHandler : IRequestHandler<GetAddress, OperationResult<Address>>
//    {
//        private readonly IDapperReadOnlyRepository<Address> _addressRepository;

//        public GetAddressHandler(IDapperReadOnlyRepository<Address> addressRepository)
//        {
//            _addressRepository = addressRepository;
//        }

//        public async Task<OperationResult<Address>> Handle(GetAddress request, CancellationToken cancellationToken)
//        {
//            var result = new OperationResult<Address>();

//            var address = await _addressRepository.GetByIdAsync(request.Id);

//            if (address == null)
//            {
//                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
//                return result;
//            }

//            result.Payload = address;
//            result.Message = "Operation success";
//            return result;
//        }
//    }
//}
