using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Cpu
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Socket { get; set; }
        public int? Family { get; set; }
        public int? Manufacturer { get; set; }
        public int? Series { get; set; }
        public int? Cores { get; set; }
        public int? Gpu { get; set; }
        public int? Year { get; set; }
        public int? RamType { get; set; }
        public int? FreqBase { get; set; }
        public int? FreqBoost { get; set; }
        public int? TechProcess { get; set; }
        public int? Tdp { get; set; }
        public int? Packaging { get; set; }
        public int? CoolerHeight { get; set; }
    }
}
