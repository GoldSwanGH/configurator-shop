using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class CategoryCpu
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Socket { get; set; }
        public int? Family { get; set; }
        public int? Manufacturer { get; set; }
        public int? Series { get; set; }
        public int? Cores { get; set; }
        public int? Threads { get; set; }
        public int? GpuChipset { get; set; }
        public int? RamTechnology { get; set; }
        public int? FreqBase { get; set; }
        public int? FreqBoost { get; set; }
        public int? TechProcess { get; set; }
        public int? Tdp { get; set; }
        public int? Packaging { get; set; }
        public bool UnlockedMultiplier { get; set; }

        public virtual SpecCpuFamily FamilyNavigation { get; set; }
        public virtual SpecGpuChipset GpuChipsetNavigation { get; set; }
        public virtual SpecManufacturer ManufacturerNavigation { get; set; }
        public virtual SpecCpuPackaging PackagingNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual SpecRamTechnology RamTechnologyNavigation { get; set; }
        public virtual SpecCpuSeries SeriesNavigation { get; set; }
        public virtual SpecSocket SocketNavigation { get; set; }
    }
}
