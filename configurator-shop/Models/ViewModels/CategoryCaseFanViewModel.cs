using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryCaseFanViewModel
    {
        public List<CategoryCaseFan> Products { get; set; }
        
        public CategoryCaseFan Filters { get; set; }

        public CategoryCaseFanViewModel()
        {
            Products = new List<CategoryCaseFan>();

            Filters = new CategoryCaseFan() {Product = new Product()};
        }
    }
}