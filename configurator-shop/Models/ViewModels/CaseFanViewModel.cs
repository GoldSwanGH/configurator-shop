using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CaseFanViewModel : ImageViewModel
    {
        public CategoryCaseFan CaseFan { get; set; }
        
        public CaseFanViewModel()
        {
            CaseFan = new CategoryCaseFan {Product = new Product()};
        }
    }
}