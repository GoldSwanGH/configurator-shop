using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryGpuViewModel
    {
        public List<CategoryGpu> Products { get; set; }
        
        public CategoryGpu Filters { get; set; }

        public CategoryGpuViewModel()
        {
            Products = new List<CategoryGpu>();

            Filters = new CategoryGpu() {Product = new Product()};
        }
    }
}