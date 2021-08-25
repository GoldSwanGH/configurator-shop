using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CategoryCaseViewModel
    {
        public List<CaseViewModel> Products { get; set; }
        
        public CategoryCase Filters { get; set; }

        public CategoryCaseViewModel()
        {
            Products = new List<CaseViewModel>();

            Filters = new CategoryCase() {Product = new Product()};
        }
    }
}