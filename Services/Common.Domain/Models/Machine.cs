using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Common.Machine")]
    public class Machine : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; private set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; private set; } = string.Empty;
        [MaxLength(26)]
        public long? ConnectionType { get; private set; }
        public long? SystemModel { get; private set; }
        public long? Type { get; private set; }
        public long? Host { get; private set; }
        [MaxLength(500)]
        public string MachineValue { get; private set; } = string.Empty;
        public long? Category { get; private set; }
        [MaxLength(50)]
        public string IpAddress { get; private set; } = string.Empty;
        [MaxLength(100)]
        public string MacAddress { get; private set; } = string.Empty;
        public long? IpPort { get; private set; }
        public long? SerialPort { get; private set; }
        public long? Unit { get; private set; }
        public bool? IsEvenParity { get; private set; }
        public long? BaudRate { get; private set; }

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
                SystemModel = systemModel,
                Type = type,
                Host = host,
                MachineValue = machineValue,
                Category = category,
                IpAddress = ipAddress,
                MacAddress = macAddress,
                IpPort = ipPort,
                SerialPort = serialPort,
                Unit = unit,
                IsEvenParity = isEvenParity,
                BaudRate = baudRate
            };
        }
    }
}
