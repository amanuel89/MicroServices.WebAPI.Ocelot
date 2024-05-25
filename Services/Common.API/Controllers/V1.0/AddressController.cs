using RideBackend.Application.Commands;

namespace RideBackend.API.Controllers.V1._0;
public class AddressController : BaseController
{
    [HttpPost("Create")]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddressDto request)
    {
        var result = await _mediator.Send(new CreateAddress { AddressName = request.AddressName, AddressType = request.AddressType, ParentID = request.ParentID });
        var Address = _mapper.Map<AddressDetailDto>(result.Payload);
        var response = new OperationResult<AddressDetailDto>() { Payload = Address, Message = result.Message, IsError = result.IsError };
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
    [HttpPut("Update/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] AddressDto request, long id)
    {
        var result = await _mediator.Send(new UpdateAddress { AddressName = request.AddressName, AddressType = request.AddressType, ParentID = request.ParentID, Id = id });
        var Address = _mapper.Map<AddressDetailDto>(result.Payload);
        var response = new OperationResult<AddressDetailDto>() { Payload = Address, Message = result.Message, IsError = result.IsError };
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] RecordStatusDto request, long id)
    {
        var result = await _mediator.Send(new UpdateAddressStatus { RecordStatus = request.Status, Id = id });
        var Address = _mapper.Map<AddressDetailDto>(result.Payload);
        var response = new OperationResult<AddressDetailDto>() { Payload = Address, Message = result.Message, IsError = result.IsError };
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteAddress { Id = id });
        var Address = _mapper.Map<AddressDetailDto>(result.Payload);
        var response = new OperationResult<AddressDetailDto>() { Payload = Address, Message = result.Message, IsError = result.IsError };
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
}
    //[HttpGet]
    //public async Task<IActionResult> GetAll(RecordStatus? recordStatus, int pageSize, int pageNumber)
    //{

    //    var result = await _mediator.Send(new GetAddresses { RecordStatus = recordStatus, PageNumber = pageNumber, PageSize = pageSize });
    //    var Address = _mapper.Map<List<AddressDetailDto>>(result.Payload);
    //    var Pagedresponse = new PagedResponse<IEnumerable<AddressDetailDto>>(Address, result.CurrentPageNumber, result.CurrentPageSize, result.TotalRecords) { Message = result.Message, IsError = result.IsError };
    //    return result.IsError ? HandleErrorResponse(result.Errors) : Ok(Pagedresponse);
    //}
    //[HttpGet("{id}")]
    //public async Task<IActionResult> Get(long id)
    //{
    //    var result = await _mediator.Send(new GetAddress { Id = id });
    //    var Address = _mapper.Map<AddressDetailDto>(result.Payload);
    //    var response = new OperationResult<AddressDetailDto>() { Payload = Address, Message = result.Message, IsError = result.IsError };
    //    return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    //}
    //[HttpGet("Search")]
    //public async Task<IActionResult> Search(string searchKey)
    //{
    //    var result = await _mediator.Send(new SearchAddress { searchKey = searchKey });
    //    var Address = _mapper.Map<List<AddressDetailDto>>(result.Payload);
    //    var response = new OperationResult<List<AddressDetailDto>>() { Payload = Address, Message = result.Message, IsError = result.IsError };
    //    return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    //}
    //[HttpGet("GetHierarchy")]
    //public async Task<IActionResult> GetHierarchy()
    //{
    //    var result = await _mediator.Send(new GetAddresssHierarchy {});
    //    var Address = _mapper.Map<List<AddressesHierarchyDto>>(result.Payload);
    //    return result.IsError ? HandleErrorResponse(result.Errors) : Ok(Address);
    //}
//}
