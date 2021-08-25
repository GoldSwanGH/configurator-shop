using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryPsuViewModel
    {
        public List<CategoryPsu> Products { get; set; }
        
        public CategoryPsu Filters { get; set; }

        public CategoryPsuViewModel()
        {
            Products = new List<CategoryPsu>();

            Filters = new CategoryPsu() {Product = new Product()};
        }
    }
}