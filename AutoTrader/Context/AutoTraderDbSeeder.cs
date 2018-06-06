using System;
using System.Collections.Generic;
using System.Linq;
using AutoTrader.Models;

namespace AutoTrader.Conntext
{
    public static class AutoTraderDbSeeder
    {

        private static readonly string[] carNames = {
            "Hyundai",
            "Chevrolet",
            "Ford",
            "Nissan",
            "Nissan",
            "Honda",
            "Toyota",
            "BMW",
            "Ferari",
            "Suzuki",
        };

        private static readonly string[] carModels = {
            "CR",
            "Accord",
            "Civic",
            "F-150",
            "GT-12",
            "Toca",
            "Strider",
            "Index",
            "Swift",
            "Rock",
        };

        public static void Init(this AutoTraderContext context)
        {
            // exit code
            if(context.Vehicles.Any())
            {
                return;
            }

            var carList = new List<Vehicle>();
            for(int x = 0; x < 30; x++)
            {
                carList.Add(GenerateRandomCar());
            }

            context.Vehicles.AddRange(carList);
            context.SaveChanges();
            
        }

        public static Vehicle GenerateRandomCar()
        {
            Random random = new Random();
            return new Vehicle()
            {
                VehicleId = Guid.NewGuid(),
                CylinderVariant = random.Next(9) + 1,
                EngineCapacity = Math.Round((Double)random.Next(3) + random.NextDouble(),1),
                Make = carNames[random.Next(carNames.Length)],
                Model = carModels[random.Next(carModels.Length)],
                Price = ((Double)random.Next(20) + 1) * 10000.00 ,
                TopSpeed = ((Double)random.Next(40000)+1) / 100
            };
        }
    }
}