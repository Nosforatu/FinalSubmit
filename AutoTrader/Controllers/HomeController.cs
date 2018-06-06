using AutoTrader.Models;
using AutoTrader.Models.ViewModels.AutoDealer;
using AutoTrader.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Controllers
{
    public class HomeController : Controller
    {
        public VehicleService vehicleService;
        public HomeController(VehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index(IndexViewModel vm)
        {

            // Base Query
            var query = vehicleService.GetVehicles();

            //filter setup
            if (vm.Filter == null)
            {
                vm.Filter = new FilterViewModel();
            }

            // Search Filter Make or Model
            if (vm.Filter.SearchCriteria != null)
            {
                query = query.Where(w => w.Make.ToLower().Contains(vm.Filter.SearchCriteria.ToLower()) || w.Model.ToLower().Contains(vm.Filter.SearchCriteria.ToLower()));
            }

            // Engine Capacity Filter
            if (vm.Filter.EnginCapacity)
            {
                query = query.Where(w => w.EngineCapacity >= 2);
            }

            // Price Range Brackets
            // Min check
            if (vm.Filter.PriceMinimum > 0)
            {
                query = query.Where(w => w.Price >= vm.Filter.PriceMinimum);
            }

            // Max Check
            if (vm.Filter.PriceMaximum > 0)
            {
                query = query.Where(w => w.Price <= vm.Filter.PriceMaximum);
            }

            // Filter Celinder Variant
            if (vm.Filter.CelinderVariant > 0)
            {
                query = query.Where(w => w.CylinderVariant.Equals(vm.Filter.CelinderVariant));
            }

            if (vm.Filter.SingularCelinderCapacity > 0)
            {
                query.Where(w => (w.EngineCapacity / w.CylinderVariant) == vm.Filter.SingularCelinderCapacity);
            }

            vm.Vehicles = await query
                .OrderBy(o => o.Model)
                .ToListAsync();

            return View(vm);
        }

        public IActionResult InsertVehicle(InsertVehicleViewModel vm)
        {
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertVehicleViewModel vm)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                vm.Message = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault().ErrorMessage;

                return RedirectToAction("InsertVehicle", vm);
            }

            Vehicle vehicle = new Vehicle()
            {
                CylinderVariant = vm.CylinderVariant,
                EngineCapacity = vm.EngineCapacity,
                Make = vm.Make,
                Model = vm.Model,
                Price = vm.Price,
                TopSpeed = vm.TopSpeed
            };

            try
            {
                await vehicleService.Insert(vehicle);
                vm.Message = "Inserted successfully";
                return RedirectToAction("Index", new IndexViewModel() { Message = "Inserted successfully" });
            }
            catch (Exception)
            {
                await vehicleService.Insert(vehicle);
                vm.Message = "Please check your data and try again";
                return View("InsertVehicle", vm);
            }

        }

        public async Task<IActionResult> UpdateVehicle(Guid VehicleId)
        {
            var vehicle = await vehicleService.GetVehicles()
                .Where(w => w.VehicleId.Equals(VehicleId))
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                return RedirectToAction("Index", new IndexViewModel() { Message = "Invalid Vehicle ID" });
            }

            return View("InsertVehicle", new InsertVehicleViewModel()
            {
                CylinderVariant = vehicle.CylinderVariant,
                EngineCapacity = vehicle.EngineCapacity,
                Model = vehicle.Model,
                Make = vehicle.Make,
                Price = vehicle.Price,
                TopSpeed = vehicle.TopSpeed,
                VehicleId = vehicle.VehicleId
            });


        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid VehicleID)
        {
            var vehicle = await vehicleService.GetVehicles().Where(w => w.VehicleId.Equals(VehicleID)).FirstOrDefaultAsync();
            if (vehicle == null)
                return RedirectToAction("Index", new IndexViewModel() { Message = "Invalid ID" });

            await vehicleService.Delete(VehicleID);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(InsertVehicleViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new IndexViewModel() { Message = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault().ErrorMessage });
            }

            var vehicle = await vehicleService.GetVehicles().Where(w => w.VehicleId.Equals(vm.VehicleId)).FirstOrDefaultAsync();
            if (vehicle == null)
            {
                return RedirectToAction("Index", new IndexViewModel() { Message = "Invalid Vehicle ID" });
            }

            Vehicle updateVehicle = new Vehicle()
            {
                CylinderVariant = vm.CylinderVariant,
                EngineCapacity = vm.EngineCapacity,
                Make = vm.Make,
                Model = vm.Model,
                Price = vm.Price,
                TopSpeed = vm.TopSpeed,
                VehicleId = vm.VehicleId
            };

            try
            {
                await vehicleService.Update(updateVehicle);
                return RedirectToAction("Index", new IndexViewModel() { Message = "Updated Successfully " });
            }
            catch (ArgumentException e)
            {
                vm.Message = e.Message;
                return RedirectToAction("Index", new IndexViewModel() { Message = e.Message });
            }


        }
    }
}
