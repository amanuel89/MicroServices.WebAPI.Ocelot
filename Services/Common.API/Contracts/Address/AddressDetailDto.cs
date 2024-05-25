using RideBackend.Domain.Common;

namespace RideBackend.API.Contracts.Addresses;

public class AddressDetailDto
{
    public long Id { get; set; }
    public string AddressName { get; set; }
    public long? ParentID { get; set; }
    public AddressType AddressType { get; set; }
    public RecordStatus RecordStatus { get; set; }
}
