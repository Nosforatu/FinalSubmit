using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Models.ViewModels.AutoDealer
{
    public class FilterViewModel
    {
        // Search Bar for Make, Model
        public string SearchCriteria { get; set; }

        // Price range braket
        public double PriceMinimum { get; set; }
        public double PriceMaximum { get; set; }
        public List<SelectListItem> PriceRange { get; set; }

        // 2L engin capacity
        public bool EnginCapacity { get; set; }

        // Celinder Variants
        public int CelinderVariant { get; set; }
        public List<SelectListItem> CelinderOptions { get; set; }

        // Singil Celinder Variant
        public double SingularCelinderCapacity { get; set; }

        public FilterViewModel()
        {
            var priceList = new List<SelectListItem>();
            var priceIncrement = 25000;
            for (int x = 1; x <= 40; x++)
            {
                priceList.Add(new SelectListItem() { Text = (priceIncrement*x).ToString(), Value = (priceIncrement * x).ToString() });
            }
            this.PriceRange = priceList;

            var celinderList = new List<SelectListItem>();
            for (int x = 1; x <= 12; x++)
            {
                celinderList.Add(new SelectListItem() { Text = x.ToString(), Value = x.ToString() });
            }
            this.CelinderOptions = celinderList;
        }
    }
}
