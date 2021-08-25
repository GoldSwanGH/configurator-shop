using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryHddViewModel
    {
        public List<CategoryHdd> Products { get; set; }
        
        public CategoryHdd Filters { get; set; }

        public CategoryHddViewModel()
        {
            Products = new List<CategoryHdd>();

            Filters = new CategoryHdd() {Product = new Product()};
        }
    }
}