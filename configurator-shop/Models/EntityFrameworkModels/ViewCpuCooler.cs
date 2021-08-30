using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ViewCpuCooler
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Manufacturer { get; set; }
        public string CoolerType { get; set; }
        public string Socket { get; set; }
        public int? Tdp { get; set; }
        public int? Height { get; set; }
        public int? Speed { get; set; }
        public int? Noise { get; set; }
        public int? Weight { get; set; }
    }
}
