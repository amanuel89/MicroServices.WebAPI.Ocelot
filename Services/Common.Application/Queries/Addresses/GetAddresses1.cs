//using RideBackend.Domain.Models;
//using Microsoft.AspNetCore.Http;

//namespace RideBackend.Application.Queries
//{
//    public class GetAddresses : IRequest<Paginated<Address>>
//    {
//        public int PageNumber { get; set; }
//        public int PageSize { get; set; }
//        public RecordStatus? RecordStatus { get; set; }
//    }

//    public class GetAddressesHandler : IRequestHandler<GetAddresses, Paginated<Address>>
//    {
//        private readonly IQueryRepository<Address> _addressRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public GetAddressesHandler(IQueryRepository<Address> addressRepository, IHttpContextAccessor httpContextAccessor)
//        {
//            _addressRepository = addressRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<Paginated<Address>> Handle(GetAddresses request, CancellationToken cancellationToken)
//        {
//            var result = new OperationResult<Paginated<Address>>();
//            var paginatedResult = new Paginated<Address>();

//            int itemsToSkip = (request.PageNumber - 1) * request.PageSize;
//            var query = $"SELECT * FROM {typeof(Address).Name} WHERE 1=1 ";

         

//            query += " AND RecordStatus <> @Deleted ORDER BY [Address].Id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

//            var parameteres = new
//            {
//                Skip = itemsToSkip,
//                Take = request.PageSize,
//                Deleted = (int)RecordStatus.Deleted
//            };

//            paginatedResult.Data = _addressRepository.Query(query, parameteres).ToList();

//            _addressRepository.GetPaginatedResponce(request.PageNumber, request.PageSize, null, ref paginatedResult);

//            if (!paginatedResult.Data.Any())
//                return null;

//            result.Payload = paginatedResult;
//            result.Message = "The operation was successful.";
//            return null;
//        }
//    }
//}
