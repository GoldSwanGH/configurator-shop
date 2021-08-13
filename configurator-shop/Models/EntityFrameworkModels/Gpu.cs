using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Gpu
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Gpu1 { get; set; }
        public int? GpuManufacturer { get; set; }
        public int? Manufacturer { get; set; }
        public int? Size { get; set; }
        public int? Vram { get; set; }
        public int? VramType { get; set; }
        public int? Bus { get; set; }
        public int? Connector { get; set; }
        public int? Hdmi { get; set; }
        public int? DisplayPort { get; set; }
        public int? MiniDisplayPort { get; set; }
        public int? Vga { get; set; }
        public int? DirectX { get; set; }
        public int? CoolingType { get; set; }
        public int? Powering { get; set; }
        public int? RecommendedPower { get; set; }

        public virtual Product Product { get; set; }
    }
}
