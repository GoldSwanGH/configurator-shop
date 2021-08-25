using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryCpuViewModel
    {
        public List<CpuViewModel> Products { get; set; }
        
        public CategoryCpu Filters { get; set; }

        public CategoryCpuViewModel()
        {
            Products = new List<CpuViewModel>();

            Filters = new CategoryCpu() {Product = new Product()};
        }
    }
}