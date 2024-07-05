using AutoMapper;
using Common.Application.Models.Common;
using CommonService.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommonService.Application.Commands
{
    public class UpdateSystemLookup : IRequest<OperationResult<bool>>
    {
        public SystemLookupUpdateRequest SystemLookupUpdateRequest { get; set; }
    }

    public class UpdateSystemLookupHandler : IRequestHandler<UpdateSystemLookup, OperationResult<bool>>
    {
        private readonly IRepositoryBase<SystemLookup> _systemLookupRepository;

        public UpdateSystemLookupHandler(IRepositoryBase<SystemLookup> systemLookupRepository)
        {
            _systemLookupRepository = systemLookupRepository ?? throw new ArgumentNullException(nameof(systemLookupRepository));
        }

        public async Task<OperationResult<bool>> Handle(UpdateSystemLookup request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            if (request.SystemLookupUpdateRequest == null)
            {
                result.AddError(ErrorCode.NotFound, "Request body cannot be empty.");
                return result;
            }
            var systemLookup = await _systemLookupRepository.FirstOrDefaultAsync(x => x.Id == request.SystemLookupUpdateRequest.Id && x.IsActive == true);
            if (systemLookup == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }
            var data = request.SystemLookupUpdateRequest;
            systemLookup.Update(data.Index, data.IsSystemDefined, data.Type, data.Description, data.Value, data.IsDefault);
            await _systemLookupRepository.UpdateAsync(systemLookup);
            result.Message = "Operation success";
            return result;
        }
    }
}
