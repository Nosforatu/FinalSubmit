using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Models.ViewModels.AutoDealer
{
    public class IndexViewModel
    {
        public List<Vehicle> Vehicles { get; set; }
        public FilterViewModel Filter { get; set; }
        public PaginationOptions GetPaginationOptions { get; set; }
        public string Message { get; set; }
    }
}
