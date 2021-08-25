using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CaseViewModel : ImageViewModel
    {
        public CategoryCase Case { get; set; }
        
        public CaseViewModel()
        {
            Case = new CategoryCase {Product = new Product()};
        }
    }
}