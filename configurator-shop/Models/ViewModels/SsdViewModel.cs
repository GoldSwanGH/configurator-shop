using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class SsdViewModel : ImageViewModel
    {
        public CategorySsd Ssd { get; set; }
        
        public SsdViewModel()
        {
            Ssd = new CategorySsd {Product = new Product()};
        }
    }
}