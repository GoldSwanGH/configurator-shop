using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryHddViewModel
    {
        public List<HddViewModel> Products { get; set; }
        
        public CategoryHdd Filters { get; set; }

        public CategoryHddViewModel()
        {
            Products = new List<HddViewModel>();

            Filters = new CategoryHdd() {Product = new Product()};
        }
    }
}