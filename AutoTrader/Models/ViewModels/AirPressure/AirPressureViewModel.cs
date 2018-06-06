using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Models.ViewModels.AirPressure
{
    public class AirPressureViewModel
    {
        public string QueryName { get; set; }
        public List<AirPresure> Values { get; set; }
    }

    //Utility class
    public class AirPresure
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
