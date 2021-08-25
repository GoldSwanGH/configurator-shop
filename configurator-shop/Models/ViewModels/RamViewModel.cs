using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class RamViewModel : ImageViewModel
    {
        public CategoryRam Ram { get; set; }
        
        public RamViewModel()
        {
            Ram = new CategoryRam {Product = new Product()};
        }
    }
}