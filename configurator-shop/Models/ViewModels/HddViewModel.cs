using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class HddViewModel : ImageViewModel
    {
        public CategoryHdd Hdd { get; set; }
        
        public HddViewModel()
        {
            Hdd = new CategoryHdd {Product = new Product()};
        }
    }
}