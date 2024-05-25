using RideBackend.Domain.Common;
using System.Collections.Generic;

namespace RideBackend.Domain.Models
{
    public class Ride : BaseEntity
    {
        public long? DriverId { get; set; } = null;
        public long? UserId { get; set; } 
        public long VehicleTypeId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public bool BookedByDriver { get; set; }=false;
        public double PickupLongitude { get; set; }
        public double PickupLatitude { get; set; }
        public double DropOffLongitude { get; set; }
        public double DropOffLatitude { get; set; }
        public double Price { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public DateTime OrderTime { get; set; }= DateTime.Now;
        public DateTime AcceptedTime { get; set; } = DateTime.MaxValue;
        public DateTime RideStartedTime { get; set; } = DateTime.MaxValue;
        public DateTime OrderCompletedTime { get; set; } = DateTime.MaxValue;
        public string CancelledReason {  get; set; } = string.Empty;
        public RideStatus RideStatus { get; set; } = RideStatus.Pending;
        public Driver? Driver { get; set; } 

        public static Ride Create(long? userid, long vehicleTypeId,double pickUpLng,double pickUpLat,double dropOffLng,double dropOffLat)
        {
            var ride= new Ride
            {
              UserId = userid,
              VehicleTypeId = vehicleTypeId,
              PickupLongitude = pickUpLng,
              PickupLatitude = pickUpLat,
              DropOffLongitude = dropOffLng,
              DropOffLatitude = dropOffLat,
              RideStatus=RideStatus.Pending,
              OrderTime=DateTime.Now,
            };
            return ride;
        
        }

        public static Ride CreateByDriver(long? userId, long vehicleTypeId, double pickUpLng, double pickUpLat, double dropOffLng, double dropOffLat,string PhoneNumber,long? driverId)
        {
            var ride = new Ride
            {
                DriverId = driverId,
                VehicleTypeId = vehicleTypeId,
                PickupLongitude = pickUpLng,
                PickupLatitude = pickUpLat,
                DropOffLongitude = dropOffLng,
                DropOffLatitude = dropOffLat,
                RideStatus = RideStatus.Pending,
                OrderTime = DateTime.Now,
                BookedByDriver=true,
                AcceptedTime=DateTime.Now,
                RideStartedTime=DateTime.Now,
                PhoneNumber=PhoneNumber,
                UserId=userId
            };
            return ride;

        }
        public void Accepted(long driverId)
        {
            this.DriverId = driverId;
            this.RideStatus = RideStatus.Accepted;
            this.AcceptedTime = DateTime.Now;
           
        }

        public void CancellBy(string reason)
        {
            this.CancelledReason = reason;
            this.OrderCompletedTime = DateTime.Now;
        }

        public void RideStarted()
        {
            this.RideStatus = RideStatus.InProgress;
            this.RideStartedTime = DateTime.Now;
        }

        public void CompleteRide()
        {
            this.RideStatus = RideStatus.Completed;
            this.OrderCompletedTime = DateTime.Now;
            
        }  

    }
}
