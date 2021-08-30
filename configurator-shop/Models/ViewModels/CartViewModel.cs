using System;
using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class CartViewModel
    {
        public List<Tuple<Product, int>> Cart { get; set; }

        public CartViewModel()
        {
            Cart = new List<Tuple<Product, int>>();
        }
    }
}