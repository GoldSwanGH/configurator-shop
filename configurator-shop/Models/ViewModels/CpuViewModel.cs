using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CpuViewModel : LoadImageViewModel
    {
        public CategoryCpu Cpu { get; set; }

        public CpuViewModel()
        {
            Cpu = new CategoryCpu {Product = new Product()};
        }
    }
}