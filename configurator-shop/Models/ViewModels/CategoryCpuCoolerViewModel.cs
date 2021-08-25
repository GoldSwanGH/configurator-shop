using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryCpuCoolerViewModel
    {
        public List<CpuCoolerViewModel> Products { get; set; }
        
        public CategoryCpuCooler Filters { get; set; }

        public CategoryCpuCoolerViewModel()
        {
            Products = new List<CpuCoolerViewModel>();

            Filters = new CategoryCpuCooler() {Product = new Product()};
        }
    }
}