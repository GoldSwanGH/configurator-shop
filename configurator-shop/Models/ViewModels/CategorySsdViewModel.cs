using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategorySsdViewModel
    {
        public List<CategorySsd> Products { get; set; }
        
        public CategorySsd Filters { get; set; }

        public CategorySsdViewModel()
        {
            Products = new List<CategorySsd>();

            Filters = new CategorySsd() {Product = new Product()};
        }
    }
}