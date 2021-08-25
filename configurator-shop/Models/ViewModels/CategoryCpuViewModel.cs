using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryCpuViewModel
    {
        public List<CategoryCpu> Products { get; set; }
        
        public CategoryCpu Filters { get; set; }

        public CategoryCpuViewModel()
        {
            Products = new List<CategoryCpu>();

            Filters = new CategoryCpu() {Product = new Product()};
        }
    }
}