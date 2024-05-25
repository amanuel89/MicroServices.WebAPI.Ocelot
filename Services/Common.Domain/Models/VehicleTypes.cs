using RideBackend.Domain.Common;
using System;
using System.Collections.Generic;

namespace RideBackend.Domain.Models
{
    public class VehicleTypes : BaseEntity
    {
        public string Name { get; private set; } =string.Empty;
        public string Image { get; private set; } = string.Empty;
        public int NumberOfPassengers { get; private set; }
        public decimal RatePerKm { get; private set; }
        public double InitialRate { get; private set; }
        public virtual List<Vehicle> Vehicles { get; set; }

        public static VehicleTypes Create(
            string name,
            string image,
            int numberOfPassengers,
            decimal ratePerKm,
            double initialRate)
        {
            return new VehicleTypes
            {
                Name = name,
                Image = image,
                NumberOfPassengers = numberOfPassengers,
                RatePerKm = ratePerKm,
                InitialRate = initialRate
            };
        }

        public void Update(
           string name,
            string image,
            int numberOfPassengers,
            decimal ratePerKm,
            double initialRate)
        {
            this.Name = name;    
            this.Image = image;
            this.NumberOfPassengers= numberOfPassengers;
            this.InitialRate = initialRate;
            this.RatePerKm= ratePerKm;
        }
    }
}
