using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class GpuViewModel : LoadImageViewModel
    {
        public CategoryGpu Gpu { get; set; }
        
        public GpuViewModel()
        {
            Gpu = new CategoryGpu {Product = new Product()};
        }
    }
}