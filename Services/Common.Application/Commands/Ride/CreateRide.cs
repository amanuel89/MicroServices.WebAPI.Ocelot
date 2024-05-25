using AutoMapper;
using IdentityServer.Application.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RideBackend.Application.Services.Helper;
using RideBackend.Domain.Dtos;
using RideBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RideBackend.Application.Commands
{
    public class CreateRide : IRequest<OperationResult<RideResponseDto>>
    {
        public bool BookedByDriver { get; set; } = false;
        public CreateRideRequestDto Ride { get; set; } = null;
        public CreateRideByDriverRequestDto RideByDriver { get; set; } = null;
    }

    public class CreateRideHandler : IRequestHandler<CreateRide, OperationResult<RideResponseDto>>
    {
        private readonly IRepositoryBase<Ride> _Ride;
        private readonly IRepositoryBase<Passenger> _passenger;
        private readonly IRedisHelper _redisHelper;
        private readonly IMapper _mapper;
        private readonly IHubContext<RideHub> _hubContext;
        private readonly IRepositoryBase<RideSettings> _RideSettings;

        public CreateRideHandler(IRepositoryBase<RideSettings> RideSettings,IRepositoryBase<Ride> ride, IMapper imapper, IRedisHelper redisHelper, IHubContext<RideHub> hubContext, IRepositoryBase<Passenger> passenger) 
        {
            this._Ride = ride;
            this._redisHelper = redisHelper;
            _mapper = imapper;
            _hubContext = hubContext; 
            _passenger = passenger;
            _RideSettings = RideSettings;
        }

        public async Task<OperationResult<RideResponseDto>> Handle(CreateRide request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<RideResponseDto>();
            var RideSettings = _RideSettings.FirstOrDefault(x => x.RecordStatus == RecordStatus.Active);
            if (RideSettings == null)
            {
                result.AddError(ErrorCode.NotFound, "Unable to find active settings.");
                return result;
            }
            try
            {
                if (request.BookedByDriver == false)
                {
                    if(request.Ride == null)
                    {
                            result.AddError(ErrorCode.NotFound, "Request cannot be empty.");
                            return result;
                        
                    }
                    var ride = Ride.Create(request.Ride.UserId, request.Ride.VehicleTypeId, request.Ride.PickupLongitude, request.Ride.PickupLatitude, request.Ride.DropOffLongitude, request.Ride.DropOffLatitude);
                    await _Ride.AddAsync(ride);
                    var nearByDrivers = await GetNearbyDrivers(request.Ride.PickupLongitude, request.Ride.PickupLatitude, 5);
                    var notification = _mapper.Map<RideNotificationDto>(ride);
                    await BroadcastToNearbyDrivers(nearByDrivers, notification);
                    var response = _mapper.Map<RideResponseDto>(ride);
                    result.Payload = response;
                    result.Message = "Operation success";
                }
                else
                {
                    if (request.RideByDriver == null)
                    {
                        result.AddError(ErrorCode.NotFound, "Request cannot be empty.");
                        return result;

                    }
                    var user = _passenger.Where(x => x.Phone == request.RideByDriver.PhoneNumber).FirstOrDefault();
                    var ride = Ride.CreateByDriver(user!=null ? user.Id:null, request.RideByDriver.VehicleTypeId, request.RideByDriver.PickupLongitude, request.RideByDriver.PickupLatitude, request.RideByDriver.DropOffLongitude, request.RideByDriver.DropOffLatitude,request.RideByDriver.PhoneNumber,request.RideByDriver.DriverId);
                    await _Ride.AddAsync(ride);
                    var nearByDrivers = await GetNearbyDrivers(request.RideByDriver.PickupLongitude, request.RideByDriver.PickupLatitude, 5);
                    var notification = _mapper.Map<RideNotificationDto>(ride);
                    await BroadcastToNearbyDrivers(nearByDrivers, notification);
                    var response = _mapper.Map<RideResponseDto>(ride);
                    result.Payload = response;
                    result.Message = "Operation success";
                }
             
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.UnknownError, $"error occurred.");
            }

            return result;
        }

        public async Task<List<DriverLocationUpdateRequest>> GetNearbyDrivers(double pickupLongitude, double pickupLatitude, double radiusKm)
        {
            // Calculate the bounding box coordinates
            var boundingBox = CalculateBoundingBox(pickupLongitude, pickupLatitude, radiusKm);

            var allnearbyDrivers = _redisHelper.GetAllFromRedis();
            List<DriverLocationUpdateRequest> driverLocations = new List<DriverLocationUpdateRequest>();

            foreach (var kvp in allnearbyDrivers)
            {
                DriverLocationUpdateRequest location = JsonConvert.DeserializeObject<DriverLocationUpdateRequest>(kvp);
                if (location != null)
                    driverLocations.Add(location);
            }

            var nearbyDrivers = driverLocations.Where(d => d.Lat >= boundingBox.MinLatitude && d.Lat <= boundingBox.MaxLatitude
                         && d.Lng >= boundingBox.MinLongitude && d.Lng <= boundingBox.MaxLongitude)
                .Select(d => new DriverLocationUpdateRequest
                {
                    DriverId = d.DriverId,
                    Lat = d.Lat,
                    Lng = d.Lng,
                    Heading = d.Heading
                })
                .ToList();

            return nearbyDrivers;
        }

        public async Task BroadcastToNearbyDrivers(List<DriverLocationUpdateRequest> nearbyDrivers, RideNotificationDto message)
        {
            foreach (var driver in nearbyDrivers)
            {
                await _hubContext.Clients.User(driver.DriverId).SendAsync("ReceiveMessage", message);
            }
        }

        public (double MinLatitude, double MaxLatitude, double MinLongitude, double MaxLongitude) CalculateBoundingBox(double centerLongitude, double centerLatitude, double radiusKm)
        {
            const double earthRadiusKm = 6371.0; // Earth radius in kilometers
            const double degreesToRadians = Math.PI / 180.0;
            const double radiansToDegrees = 180.0 / Math.PI;

            // Convert radius from kilometers to radians
            double radiusRadians = radiusKm / earthRadiusKm;

            // Convert the center point coordinates to radians
            double centerLatitudeRadians = centerLatitude * degreesToRadians;
            double centerLongitudeRadians = centerLongitude * degreesToRadians;

            // Calculate the minimum and maximum latitude and longitude of the bounding box
            double minLatitude = centerLatitudeRadians - radiusRadians;
            double maxLatitude = centerLatitudeRadians + radiusRadians;

            double deltaLongitude = Math.Asin(Math.Sin(radiusRadians) / Math.Cos(centerLatitudeRadians));
            double minLongitude = centerLongitudeRadians - deltaLongitude;
            double maxLongitude = centerLongitudeRadians + deltaLongitude;

            // Convert back to degrees
            minLatitude *= radiansToDegrees;
            maxLatitude *= radiansToDegrees;
            minLongitude *= radiansToDegrees;
            maxLongitude *= radiansToDegrees;

            return (minLatitude, maxLatitude, minLongitude, maxLongitude);
        }
    }
}
