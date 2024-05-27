using ConsigneeService.Domain.Common;

namespace ConsigneeService.Api.Contracts.Common
{
    public class StatusRequest
    {
        public RecordStatus status { get; set; }
    }
}
