using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Machine", Schema = "Common")]
    public class Machine : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; private set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; private set; } = string.Empty;
        [MaxLength(26)]
        public long? ConnectionType { get; private set; }
        public long? SystemModelId { get; private set; } //syslookup
        public long? TypeId { get; private set; }//syslookup
        public long? HostMachineId { get; private set; } //parentid
        [MaxLength(500)]
        public string MachineValue { get; private set; } = string.Empty;
        public long? CategoryId { get; private set; } //category
        [MaxLength(50)]
        public string IpAddress { get; private set; } = string.Empty;
        [MaxLength(100)]
        public string MacAddress { get; private set; } = string.Empty;
        public long? IpPort { get; private set; }
        public long? SerialPort { get; private set; }
        public long? OrganizationUnitId { get; private set; }
        public bool? IsEvenParity { get; private set; }
        public long? BaudRate { get; private set; }
        public virtual Machine HostMachine { get; set; }
        public virtual Category Category { get; set; }
        public virtual SystemLookup SystemModel { get; set; }
        public virtual SystemLookup Type { get; set; }
        public virtual OrganizationUnit OrganizationUnit { get; set; }
        public static Machine Create(
            string name,
            string description,
            long? connectionType,
            long? systemModel,
            long? type,
            long? host,
            string machineValue,
            long? category,
            string ipAddress,
            string macAddress,
            long? ipPort,
            long? serialPort,
            long? unit,
            bool? isEvenParity,
            long? baudRate)
        {
            return new Machine
            {
                Name = name,
                Description = description,
                ConnectionType = connectionType,
                SystemModelId = systemModel,
                TypeId = type,
                HostMachineId = host,
                MachineValue = machineValue,
                CategoryId = category,
                IpAddress = ipAddress,
                MacAddress = macAddress,
                IpPort = ipPort,
                SerialPort = serialPort,
                OrganizationUnitId = unit,
                IsEvenParity = isEvenParity,
                BaudRate = baudRate
            };
        }
    }
}
