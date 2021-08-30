using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ViewGpu
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string GpuChipset { get; set; }
        public string Manufacturer { get; set; }
        public string GpuManufacturer { get; set; }
        public int? Size { get; set; }
        public int? Vram { get; set; }
        public int? BaseFreq { get; set; }
        public int? BoostFreq { get; set; }
        public string VramType { get; set; }
        public int? BusWidth { get; set; }
        public string Connector { get; set; }
        public int? Hdmi { get; set; }
        public int? DisplayPort { get; set; }
        public int? MiniDisplayPort { get; set; }
        public int? Vga { get; set; }
        public string DirectX { get; set; }
        public string GpuPinPowering { get; set; }
        public int? RecommendedPower { get; set; }
    }
}
