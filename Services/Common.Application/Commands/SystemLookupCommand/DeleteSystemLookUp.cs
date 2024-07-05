using Common.Application.Models.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Application.Commands.SystemLookupCommand
{
    public class DeleteSystemLookup : IRequest<OperationResult<bool>>
    {
        public long Id { get; set; }
    }

    public class DeleteSystemLookupHandler : IRequestHandler<DeleteSystemLookup, OperationResult<bool>>
    {
        private readonly IRepositoryBase<SystemLookup> _systemLookupRepository;

        public DeleteSystemLookupHandler(IRepositoryBase<SystemLookup> systemLookupRepository)
        {
            _systemLookupRepository = systemLookupRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteSystemLookup request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            var systemLookup = await _systemLookupRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (systemLookup == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }

            var deleteResult = await _systemLookupRepository.RemoveAsync(systemLookup, cancellationToken);
            if (deleteResult)
            {
                result.Payload = true;
                result.Message = "The Record is Deleted Successfully";
            }
            else
            {
                result.AddError(ErrorCode.UnknownError, "Error occurred while deleting the record.");
            }

            return result;
        }
    }
}
