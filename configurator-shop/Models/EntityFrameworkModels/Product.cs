using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Product
    {
        public Product()
        {
            ConfigurationLists = new HashSet<ConfigurationList>();
            OrderCarts = new HashSet<OrderCart>();
        }

        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public virtual ProductType Type { get; set; }
        public virtual ICollection<ConfigurationList> ConfigurationLists { get; set; }
        public virtual ICollection<OrderCart> OrderCarts { get; set; }
    }
}
