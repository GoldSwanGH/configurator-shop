using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryPsuViewModel
    {
        public List<PsuViewModel> Products { get; set; }
        
        public CategoryPsu Filters { get; set; }

        public CategoryPsuViewModel()
        {
            Products = new List<PsuViewModel>();

            Filters = new CategoryPsu() {Product = new Product()};
        }
    }
}