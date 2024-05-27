namespace ConsigneeService.Application.Models
{

    public class DriverVehicleResponse
    {
        public long Id { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public string VehicleMaker { get;  set; } = string.Empty;
        public string Model { get;  set; } = string.Empty;
        public string ManufacturingYear { get;  set; } = string.Empty;
        public long VehicleTypeId { get;  set; }
        public string PlateNumber { get;  set; } = string.Empty;
        public string Color { get;  set; } = string.Empty;
        public string SideOne { get;  set; } = string.Empty;
        public string SideTwo { get;  set; } = string.Empty;
        public string Front { get;  set; } = string.Empty;
        public string Rear { get;  set; } = string.Empty;
        public string Insurance { get;  set; } = string.Empty;
        public string PowerOfAttorney { get;  set; } = string.Empty;
        public bool IsOwner { get;  set; }
        public string Libre { get;  set; } = string.Empty;
        public long? DriverId { get;  set; }
        public DateTime RegisteredDate { get; private set; }
        public virtual VehicleTypesDTo? VehicleType { get; private set; }

    }
    
    }

