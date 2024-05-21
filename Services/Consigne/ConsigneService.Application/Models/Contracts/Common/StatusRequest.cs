using ConsigneService.Domain.Common;

namespace ConsigneService.Application.Models.Contracts.Common
{
    public class StatusRequest
    {
        public RecordStatus status { get; set; }
    }
}
