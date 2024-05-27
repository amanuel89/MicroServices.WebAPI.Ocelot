using ConsigneeService.Domain.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Common.Application.Models.Common;

namespace ConsigneeService.Application.Queries
{
    public class GetBankList : IRequest<OperationResult<PagedResponse<IEnumerable<BankResponseDTO>>>>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public RecordStatus? RecordStatus { get; set; }
    }

    public class GetBankListHandler : IRequestHandler<GetBankList, OperationResult<PagedResponse<IEnumerable<BankResponseDTO>>>>
    {
        private readonly IRepositoryBase<Bank> _BankRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetBankListHandler(IRepositoryBase<Bank> BankRepository, IMapper mapper)
        {
            _BankRepository = BankRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PagedResponse<IEnumerable<BankResponseDTO>>>> Handle(GetBankList request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResponse<IEnumerable<BankResponseDTO>>>();

            var query = _BankRepository.Where(x => x.RecordStatus != RecordStatus.Deleted);

            if (request.RecordStatus.HasValue)
            {
                query = query.Where(x => x.RecordStatus == request.RecordStatus.Value);
            }

            var count = await query.CountAsync();
            query = query.OrderByDescending(x => x.LastUpdateDate);

            IEnumerable<Bank> data;
            if (request.PageSize == 0 || request.PageNumber == 0)
            {
                data = await query.AsNoTracking().ToListAsync();
            }
            else
            {
                data = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .AsNoTracking()
                                  .ToListAsync();
            }

            if (!data.Any())
            {
                result.AddError(ErrorCode.NotFound, "No record found");
                result.IsError = true;
            }

            var response = _mapper.Map<List<BankResponseDTO>>(data);
            result.Payload = new PagedResponse<IEnumerable<BankResponseDTO>>(response, request.PageNumber, request.PageSize, count);
            result.Message = "Operation Success";
            return result;
        }

    }
}
