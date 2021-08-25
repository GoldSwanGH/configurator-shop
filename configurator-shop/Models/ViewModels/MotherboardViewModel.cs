using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class MotherboardViewModel : LoadImageViewModel
    {
        public CategoryMotherboard Motherboard { get; set; }
        
        public MotherboardViewModel()
        {
            Motherboard = new CategoryMotherboard {Product = new Product()};
        }
    }
}