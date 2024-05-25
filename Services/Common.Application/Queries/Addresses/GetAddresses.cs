//using RideBackend.Domain.Models;
//using Microsoft.AspNetCore.Http;

//namespace RideBackend.Application.Queries
//{
//    public class GetAddresses : IRequest<PagedResponse<IEnumerable<Address>>>
//    {
//        public int PageNumber { get; set; }
//        public int PageSize { get; set; }
//        public string Search { get; set; }= string.Empty;
//        public RecordStatus? RecordStatus { get; set; }
//    }

//    public class GetAddressesHandler : IRequestHandler<GetAddresses, PagedResponse<IEnumerable<Address>>>
//    {
//        private readonly IDapperReadOnlyRepository<Address> _addressRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public GetAddressesHandler(IDapperReadOnlyRepository<Address> addressRepository, IHttpContextAccessor httpContextAccessor)
//        {
//            _addressRepository = addressRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<PagedResponse<IEnumerable<Address>>> Handle(GetAddresses request, CancellationToken cancellationToken)
//        {
//            var parameters = new { RecordStatus = request.RecordStatus , Search=string.Empty };
//            var sqlConditions = new List<string> { "RecordStatus=@RecordStatus" };

//            if (!string.IsNullOrEmpty(request.Search))
//            {
//                parameters = new { parameters.RecordStatus, Search = $"%{request.Search}%" };
//                sqlConditions.Add("Column1 LIKE @Search1");
//            }

//            var (data, count) = await _addressRepository.GetPageAsync(
//                request.PageNumber,
//                request.PageSize,
//                "LastUpdateDate",
//                SortDirection.DESC,
//                string.Join(" AND ", sqlConditions),
//                parameters, new List<string> { "Address" }
//            );

//            return new PagedResponse<IEnumerable<Address>>(data, request.PageNumber, request.PageSize, count);
//        }
//    }
//}
