using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryRamViewModel
    {
        public List<RamViewModel> Products { get; set; }
        
        public CategoryRam Filters { get; set; }

        public CategoryRamViewModel()
        {
            Products = new List<RamViewModel>();

            Filters = new CategoryRam() {Product = new Product()};
        }
    }
}