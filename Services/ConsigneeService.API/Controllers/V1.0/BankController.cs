

using ConsigneeService.API.Contracts.Common;
using ConsigneeService.Application.Commands;
using ConsigneeService.Application.Models;
using ConsigneeService.Application.Queries;
using ConsigneeService.Domain.Common;

namespace ConsigneeService.API.Controllers.V1._0;
public class BankController : BaseController
{
    [HttpPost("Create")]
    [ValidateModel]
    public async Task<IActionResult> Create([FromForm] BankRequestDTO request)
    {
        var result = await _mediator.Send(new CreateBanks { Bank = request });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("Update/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromForm] BankUpdateDTO request, long id)
    {
        var result = await _mediator.Send(new UpdateBank { Bank = request, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] RecordStatusDto request, long id)
    {
        var result = await _mediator.Send(new UpdateBanksStatus { RecordStatus = request.Status, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteBanks { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(RecordStatus? recordStatus, int pageSize = 0, int pageNumber = 0)
    {

        var result = await _mediator.Send(new GetBankList { RecordStatus = recordStatus, PageNumber = pageNumber, PageSize = pageSize });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _mediator.Send(new GetBank { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

}
