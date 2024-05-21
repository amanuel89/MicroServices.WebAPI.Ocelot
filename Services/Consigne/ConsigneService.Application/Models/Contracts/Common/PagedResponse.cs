
using System.Collections.Generic;
using System.Text;

namespace ConsigneService.Application.Models.Contracts.Common;

public class PagedResponse<T> : OperationResult<T>
{
    public int CurrentPageNumber { get; set; }
    public int CurrentPageSize { get; set; }
    public long TotalRecords { get; set; }

    public PagedResponse(T data, int pageNumber, int pageSize, long total)
    {
        CurrentPageNumber = pageNumber;
        CurrentPageSize = pageSize;
        Payload = data;
        Message = null;
        TotalRecords = total;
    }
}
