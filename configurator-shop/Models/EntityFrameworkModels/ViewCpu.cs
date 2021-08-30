using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ViewCpu
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Family { get; set; }
        public string Manufacturer { get; set; }
        public string RamTechnology { get; set; }
        public string Packaging { get; set; }
        public string Series { get; set; }
        public string Socket { get; set; }
        public string GpuChipset { get; set; }
        public int? Cores { get; set; }
        public int? FreqBase { get; set; }
        public int? FreqBoost { get; set; }
        public int? TechProcess { get; set; }
        public int? Tdp { get; set; }
        public bool UnlockedMultiplier { get; set; }
        public int? Threads { get; set; }
    }
}
