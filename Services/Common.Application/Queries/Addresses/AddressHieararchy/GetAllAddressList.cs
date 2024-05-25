//using RideBackend.Application.Models;
//using RideBackend.Domain.Models;


//namespace RideBackend.Application.Queries.Addresses.AddressHieararchy;
//public class GetAddresssHierarchy : IRequest<OperationResult<List<Address>>>
//{
//}
//public class GetAddresssHierarchyHandler : IRequestHandler<GetAddresssHierarchy, OperationResult<List<Address>>>
//{
//    private readonly IRepositoryBase<Address> _Address;
//    public GetAddresssHierarchyHandler(IRepositoryBase<Address> _Address) => this._Address = _Address;
//    public async Task<OperationResult<List<Address>>> Handle(GetAddresssHierarchy request, CancellationToken cancellationToken)
//    {
//        var result = new OperationResult<List<Address>>();
//        var Addresss = _Address.Where(x =>x.RecordStatus == RecordStatus.Active).ToList();

//        if (Addresss.Count() == 0)
//        {
//            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
//            return result;
//        }
//        List<Address> hierarchy = new List<Address>();
//        hierarchy = Addresss.Where(c => c.ParentID == null)
//                        .Select(c => new Address()
//                        {
//                            data = c,
//                            children = GetChildren(Addresss, c.Id)
//                        }).ToList();
//        HieararchyWalk(hierarchy);
//        SetNodeKey(hierarchy, "");
//        result.Payload = hierarchy;
//        result.Message = "Operation success";
//        return result;
//    }
//    void SetNodeKey(List<Address> hierarchy, string parentkey)
//    {
//        long k = 0;
//        foreach (var childNode in hierarchy)
//        {
//            childNode.key = parentkey == "" ? k.ToString() : parentkey + "-" + k.ToString();
//            k++;
//            SetNodeKey(childNode.children, childNode.key);
//        }
//    }

//    public List<Address> GetChildren(List<Address> Addresss, long parentId)
//    {
//        foreach (var Address in Addresss)
//        {
//            Address.HierarchyData(Address);
//        };
//        return Addresss
//                .Where(c => c.ParentID == parentId)
//                .Select(c => new Address
//                {
//                    data = c,
//                    children = GetChildren(Addresss, c.Id)
//                }).ToList();
//    }

//    public void HieararchyWalk(List<Address> hierarchy)
//    {
//        if (hierarchy != null)
//        {
//            foreach (var item in hierarchy)
//            {
//                item.HierarchyData(item);
//                if (item.children.Count != 0)
//                {
//                    HieararchyWalk(item.children);
//                }
//            }
//        }
//    }


//}

