using Common.Application.Commands.ExchangeRateCommand;
using CommonService.Application.Commands;

namespace CommonService.API.Controllers.V1._0
{
    public class ExchangeRateController : BaseController
    {
        #region Command
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ExchangeRateCreateRequest request)
        {
            var result = await _mediator.Send(new ExchangeRateCreateCommand { ExchangeRateRequest = request });
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] ExchangeRateUpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateExchangeRate { ExchangeRateUpdateRequest = request });
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
        }

        [HttpPut("UpdateStatus/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateStatus(long id)
        {
            var result = await _mediator.Send(new UpdateExchangeRateStatus { Id = id });
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        [ValidateModel]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeleteExchangeRate { Id = id });
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
        }
        #endregion
    }
}
