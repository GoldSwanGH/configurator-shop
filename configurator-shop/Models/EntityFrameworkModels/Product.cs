using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Product
    {
        public Product()
        {
            CaseFans = new HashSet<CaseFan>();
            Cases = new HashSet<Case>();
            ConfigurationLists = new HashSet<ConfigurationList>();
            CpuCoolers = new HashSet<CpuCooler>();
            Cpus = new HashSet<Cpu>();
            Gpus = new HashSet<Gpu>();
            Hdds = new HashSet<Hdd>();
            Motherboards = new HashSet<Motherboard>();
            OrderCarts = new HashSet<OrderCart>();
            Psus = new HashSet<Psu>();
            Rams = new HashSet<Ram>();
            Ssds = new HashSet<Ssd>();
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
        public virtual ICollection<CaseFan> CaseFans { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
        public virtual ICollection<ConfigurationList> ConfigurationLists { get; set; }
        public virtual ICollection<CpuCooler> CpuCoolers { get; set; }
        public virtual ICollection<Cpu> Cpus { get; set; }
        public virtual ICollection<Gpu> Gpus { get; set; }
        public virtual ICollection<Hdd> Hdds { get; set; }
        public virtual ICollection<Motherboard> Motherboards { get; set; }
        public virtual ICollection<OrderCart> OrderCarts { get; set; }
        public virtual ICollection<Psu> Psus { get; set; }
        public virtual ICollection<Ram> Rams { get; set; }
        public virtual ICollection<Ssd> Ssds { get; set; }
    }
}
