using Common.Application.Commands.SystemLookupCommand;
using CommonService.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CommonService.API.Controllers.V1._0
{
    public class SystemLookupController : BaseController
    {
        private readonly IMediator _mediator;

        public SystemLookupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Command

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] SystemLookupCreateRequest request)
        {
            var result = await _mediator.Send(new SystemLookupCreateCommand { SystemLookupRequest = request });
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] SystemLookupUpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateSystemLookup { SystemLookupUpdateRequest = request });
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
        }

        [HttpPut("UpdateStatus/{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(long id)
        {
            var result = await _mediator.Send(new UpdateSystemLookupStatus { Id = id });
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        [ValidateModel]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeleteSystemLookup { Id = id });
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
        }

        #endregion
    }
}
