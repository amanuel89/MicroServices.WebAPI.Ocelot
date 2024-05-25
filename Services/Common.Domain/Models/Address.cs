
using RideBackend.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RideBackend.Domain.Models
{
    public class Address : BaseEntity
    {
        public string AddressName { get; private set; }
        public long? ParentID { get; private set; } 
        public AddressType AddressType { get; private set; }
        public virtual Address Parent { get; set; }
        [NotMapped]
        public virtual string key { get; set; }
        [NotMapped]
        public virtual Address data { get; set; }
        [NotMapped]
        public virtual List<Address> children { get; set; }
        public static Address Create(string addressname,long? parentid,AddressType addresstype)
        {

            var Address = new Address
            {
               AddressName=addressname,
               AddressType=addresstype,
               ParentID=parentid,
            };
            return Address;
        }
        public void Update(string addressname, long? parentid, AddressType addresstype)
        {
            AddressName = addressname;
            AddressType = addresstype;
            ParentID = parentid;

        }

        public void HierarchyData(Address data)
        {
            data.Parent = null;
        }


    }
}
