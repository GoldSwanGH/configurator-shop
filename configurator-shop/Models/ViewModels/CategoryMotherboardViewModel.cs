using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryMotherboardViewModel
    {
        public List<CategoryMotherboard> Products { get; set; }
        
        public CategoryMotherboard Filters { get; set; }

        public CategoryMotherboardViewModel()
        {
            Products = new List<CategoryMotherboard>();

            Filters = new CategoryMotherboard() {Product = new Product()};
        }
    }
}