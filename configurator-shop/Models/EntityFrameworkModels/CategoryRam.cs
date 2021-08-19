using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class CategoryRam
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? RamType { get; set; }
        public int? Modules { get; set; }
        public int? Speed { get; set; }
        public int? RamTechnology { get; set; }

        public virtual SpecManufacturer ManufacturerNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual SpecRamTechnology RamTechnologyNavigation { get; set; }
        public virtual SpecRamType RamTypeNavigation { get; set; }
    }
}
