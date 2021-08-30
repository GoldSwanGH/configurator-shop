using System;
using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        
        public List<Tuple<string, string>> Specs { get; set; }

        public ProductViewModel()
        {
            Specs = new List<Tuple<string, string>>();
        }
    }
}