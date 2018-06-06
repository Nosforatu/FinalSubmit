using AutoTrader.Models;
using AutoTrader.Models.ViewModels.AirPressure;
using AutoTrader.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Controllers
{
    public class ChartController : Controller
    {
        public VehicleService vehicleService;

        private static readonly int DEFUALT_CELINDERS = 8;

        public ChartController(VehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> AirPressure()
        {

            AirPressureViewModel vm = new AirPressureViewModel();
            vm.QueryName = "Air Pressure";

            vm.Values = await vehicleService.GetVehicles()
                .Where(w => w.CylinderVariant > 0)
                .Select(s => new AirPresure()
                {
                    Name = String.Format("{0} {1}", s.Make,s.Model),
                    Value = Math.Round((Double)s.EngineCapacity / 14.7 / s.CylinderVariant,5)
                })
                .Where(w => w.Value > 0.025)
                .OrderBy(o => o.Value)
                .ToListAsync();



            return new JsonResult(vm);
        }

        [HttpGet]
        public async Task<JsonResult> AirPressureByCelinder()
        {
            AirPressureViewModel vm = new AirPressureViewModel();
            vm.QueryName = "V8 Celinder cars";
            vm.Values = await vehicleService.GetVehicles()
                .Where(w => w.CylinderVariant > 0)
                .Select(s => new AirPresure()
                {
                    Name = String.Format("{0} {1}", s.Make, s.Model),
                    Value = Math.Round((Double)s.EngineCapacity / 14.7 / s.CylinderVariant, 5)
                })
                .OrderBy(o => o.Value)
                .ToListAsync();

            return new JsonResult(vm);
        }
    }
}
