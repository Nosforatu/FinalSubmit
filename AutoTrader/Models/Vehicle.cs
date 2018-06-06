using System;
using System.ComponentModel.DataAnnotations;

namespace AutoTrader.Models
{
    public class Vehicle
    {
        [Key]
        public Guid VehicleId { get; set; }

        [Required]
        [StringLength(30)]
        public string Model { get; set; }

        [Required]
        [StringLength(30)]
        public string Make { get; set; }

        [Required]
        [Range(0.1, Double.MaxValue)]
        public double EngineCapacity { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CylinderVariant { get; set;  }

        [Required]
        public double TopSpeed { get; set; }

        [Required]
        public double Price { get; set; }
    }
}