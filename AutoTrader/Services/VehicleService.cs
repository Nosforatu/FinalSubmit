using AutoTrader.Conntext;
using AutoTrader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Services
{
    public class VehicleService
    {
        private AutoTraderContext db;

        public VehicleService(AutoTraderContext db)
        {
            this.db = db;
        }

        // Insert
        public async Task<Vehicle> Insert(Vehicle item)
        {
            item.VehicleId = Guid.NewGuid();
            db.Vehicles.Add(item);
            await db.SaveChangesAsync();
            return item;
        }

        public async Task Delete(Guid VehicleID)
        {
            var vehicle = await db.Vehicles.FindAsync(VehicleID);
            if(vehicle == null)
            {
                throw new ArgumentException("Invalid VehicleID", nameof(Vehicle.VehicleId));
            }

            db.Vehicles.Remove(vehicle);
            await db.SaveChangesAsync();
        }
        
        public async Task<Vehicle> Update(Vehicle item)
        {
            var vehicle = await db.Vehicles.FindAsync(item.VehicleId);
            if (vehicle == null)
            {
                throw new ArgumentException("Invalid VehicleID", nameof(Vehicle.VehicleId));
            }

            vehicle.CylinderVariant = item.CylinderVariant;
            vehicle.EngineCapacity = item.EngineCapacity;
            vehicle.Make = item.Make;
            vehicle.Model = item.Model;
            vehicle.Price = item.Price;
            vehicle.TopSpeed = item.TopSpeed;
            
            db.Vehicles.Update(vehicle);
            await db.SaveChangesAsync();

            return vehicle;
        }

        // GET
        public IQueryable<Vehicle> GetVehicles()
        {
            return db.Vehicles;
        }
    }
}
