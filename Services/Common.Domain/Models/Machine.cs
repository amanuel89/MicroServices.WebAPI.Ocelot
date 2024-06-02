using CommonService.Domain.Common;

namespace CommonService.Domain.Models
{
    public class Machine : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public long? ConnectionType { get; private set; }
        public long? SystemModel { get; private set; }
        public long? Type { get; private set; }
        public long? Host { get; private set; }
        public string MachineValue { get; private set; } = string.Empty;
        public long? Category { get; private set; }
        public string IpAddress { get; private set; } = string.Empty;
        public string MacAddress { get; private set; } = string.Empty;
        public long? IpPort { get; private set; }
        public long? SerialPort { get; private set; }
        public long? Unit { get; private set; }
        public bool? IsEvenParity { get; private set; }
        public long? BaudRate { get; private set; }
        public bool? IsActive { get; private set; }
        public DateTime? CreatedOn { get; private set; }
        public long? CreatedBy { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public long? UpdatedBy { get; private set; }
        public long? TrxnUnit { get; private set; }
        public string Remark { get; private set; } = string.Empty;

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
            long? baudRate,
            bool? isActive,
            DateTime? createdOn,
            long? createdBy,
            DateTime? updatedOn,
            long? updatedBy,
            long? trxnUnit,
            string remark)
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
                BaudRate = baudRate,
                IsActive = isActive,
                CreatedOn = createdOn,
                CreatedBy = createdBy,
                UpdatedOn = updatedOn,
                UpdatedBy = updatedBy,
                TrxnUnit = trxnUnit,
                Remark = remark
            };
        }
    }
}
