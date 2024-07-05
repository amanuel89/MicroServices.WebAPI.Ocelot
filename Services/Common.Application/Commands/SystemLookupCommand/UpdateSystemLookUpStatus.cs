using Common.Application.Models.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Application.Commands.SystemLookupCommand
{
    public class UpdateSystemLookupStatus : IRequest<OperationResult<bool>>
    {
        public long Id { get; set; }
    }

    public class UpdateSystemLookupStatusHandler : IRequestHandler<UpdateSystemLookupStatus, OperationResult<bool>>
    {
        private readonly IRepositoryBase<SystemLookup> _systemLookupRepository;

        public UpdateSystemLookupStatusHandler(IRepositoryBase<SystemLookup> systemLookupRepository)
        {
            _systemLookupRepository = systemLookupRepository;
        }

        public async Task<OperationResult<bool>> Handle(UpdateSystemLookupStatus request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            var systemLookup = await _systemLookupRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (systemLookup == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }

            systemLookup.UpdateRecordStatus(!systemLookup.IsActive);
            var updateResult = await _systemLookupRepository.UpdateAsync(systemLookup);

            if (updateResult)
            {
                result.Payload = true;
                result.Message = systemLookup.IsActive
                    ? "The Record is Activated Successfully"
                    : "The Record is Deactivated Successfully";
            }
            else
            {
                result.AddError(ErrorCode.UnknownError, "Error occurred while updating the record.");
            }

            result.Payload = updateResult;
            return result;
        }
    }
}
