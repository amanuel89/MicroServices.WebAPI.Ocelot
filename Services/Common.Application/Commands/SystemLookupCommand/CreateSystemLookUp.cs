using AutoMapper;
using Common.Application.Models.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Application.Commands.SystemLookupCommand
{
    public class SystemLookupCreateCommand : IRequest<OperationResult<bool>>
    {
        public SystemLookupCreateRequest SystemLookupRequest { get; set; }
    }

    public class CreateSystemLookupHandler : IRequestHandler<SystemLookupCreateCommand, OperationResult<bool>>
    {
        private readonly IRepositoryBase<SystemLookup> _systemLookupRepository;

        public CreateSystemLookupHandler(IRepositoryBase<SystemLookup> systemLookupRepository)
        {
            _systemLookupRepository = systemLookupRepository;
        }

        public async Task<OperationResult<bool>> Handle(SystemLookupCreateCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            try
            {
                if (request.SystemLookupRequest == null)
                {
                    result.AddError(ErrorCode.ValidationError, "Request body cannot be empty");
                    return result;
                }

                var data = request.SystemLookupRequest;
                var systemLookup = SystemLookup.Create(data.Index, data.IsSystemDefined, data.Type, data.Description, data.Value, data.IsDefault);
                await _systemLookupRepository.AddAsync(systemLookup);
                result.Payload = true;
                result.Message = "Operation success";
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.UnknownError, "An error occurred. SystemLookup not registered.");
            }

            return result;
        }
    }
}
