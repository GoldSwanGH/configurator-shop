using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Product
    {
        public Product()
        {
            CategoryCaseFans = new HashSet<CategoryCaseFan>();
            CategoryCases = new HashSet<CategoryCase>();
            CategoryCpuCoolers = new HashSet<CategoryCpuCooler>();
            CategoryCpus = new HashSet<CategoryCpu>();
            CategoryGpus = new HashSet<CategoryGpu>();
            CategoryHdds = new HashSet<CategoryHdd>();
            CategoryMotherboards = new HashSet<CategoryMotherboard>();
            CategoryPsus = new HashSet<CategoryPsu>();
            CategoryRams = new HashSet<CategoryRam>();
            CategorySsds = new HashSet<CategorySsd>();
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
        public virtual ICollection<CategoryCaseFan> CategoryCaseFans { get; set; }
        public virtual ICollection<CategoryCase> CategoryCases { get; set; }
        public virtual ICollection<CategoryCpuCooler> CategoryCpuCoolers { get; set; }
        public virtual ICollection<CategoryCpu> CategoryCpus { get; set; }
        public virtual ICollection<CategoryGpu> CategoryGpus { get; set; }
        public virtual ICollection<CategoryHdd> CategoryHdds { get; set; }
        public virtual ICollection<CategoryMotherboard> CategoryMotherboards { get; set; }
        public virtual ICollection<CategoryPsu> CategoryPsus { get; set; }
        public virtual ICollection<CategoryRam> CategoryRams { get; set; }
        public virtual ICollection<CategorySsd> CategorySsds { get; set; }
        public virtual ICollection<ConfigurationList> ConfigurationLists { get; set; }
        public virtual ICollection<OrderCart> OrderCarts { get; set; }
    }
}
