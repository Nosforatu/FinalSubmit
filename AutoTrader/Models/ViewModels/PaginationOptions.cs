using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Models.ViewModels
{
    public class PaginationOptions
    {
        public int CurrentPage { get; set; }
        public int Items { get; set; }
        public int NumberOfPages { get; set; }
    }
}
