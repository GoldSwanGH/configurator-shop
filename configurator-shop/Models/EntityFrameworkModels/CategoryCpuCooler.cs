using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class CategoryCpuCooler
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? CoolerType { get; set; }
        public int? Socket { get; set; }
        public int? Tdp { get; set; }
        public int? Height { get; set; }
        public int? FanCount { get; set; }
        public int? Speed { get; set; }
        public int? Noise { get; set; }
        public int? Weight { get; set; }

        public virtual SpecCoolerType CoolerTypeNavigation { get; set; }
        public virtual SpecManufacturer ManufacturerNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual SpecSocket SocketNavigation { get; set; }
    }
}
