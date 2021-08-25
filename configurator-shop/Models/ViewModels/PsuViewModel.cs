using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class PsuViewModel : ImageViewModel
    {
        public CategoryPsu Psu { get; set; }
        
        public PsuViewModel()
        {
            Psu = new CategoryPsu {Product = new Product()};
        }
    }
}