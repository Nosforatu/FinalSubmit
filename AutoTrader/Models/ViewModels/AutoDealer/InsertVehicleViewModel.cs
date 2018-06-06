using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Models.ViewModels.AutoDealer
{
    public class InsertVehicleViewModel
    {
        [Required]
        [StringLength(30)]
        public string Model { get; set; }

        [Required]
        [StringLength(30)]
        public string Make { get; set; }

        [Required]
        public double EngineCapacity { get; set; }

        [Required]
        public int CylinderVariant { get; set; }

        [Required]
        public double TopSpeed { get; set; }

        [Required]
        public double Price { get; set; }

        public Guid VehicleId { get; set; }

        public string Message { get; set;  }
    }
}
