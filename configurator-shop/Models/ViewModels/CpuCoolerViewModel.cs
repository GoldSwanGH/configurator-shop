using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CpuCoolerViewModel : LoadImageViewModel
    {
        public CategoryCpuCooler CpuCooler { get; set; }
        
        public CpuCoolerViewModel()
        {
            CpuCooler = new CategoryCpuCooler {Product = new Product()};
        }
    }
}