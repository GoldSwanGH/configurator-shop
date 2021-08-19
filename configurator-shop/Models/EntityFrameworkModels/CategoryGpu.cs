using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class CategoryGpu
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? GpuChipset { get; set; }
        public int? GpuManufacturer { get; set; }
        public int? Manufacturer { get; set; }
        public int? Size { get; set; }
        public int? Vram { get; set; }
        public int? VramType { get; set; }
        public int? BusWidth { get; set; }
        public int? Connector { get; set; }
        public int? Hdmi { get; set; }
        public int? DisplayPort { get; set; }
        public int? MiniDisplayPort { get; set; }
        public int? Vga { get; set; }
        public int? DirectX { get; set; }
        public int? GpuPinPowering { get; set; }
        public int? RecommendedPower { get; set; }

        public virtual SpecGpuConnectorType ConnectorNavigation { get; set; }
        public virtual SpecDirectxType DirectXNavigation { get; set; }
        public virtual SpecGpuChipset GpuChipsetNavigation { get; set; }
        public virtual SpecManufacturer GpuManufacturerNavigation { get; set; }
        public virtual SpecGpuPinPowering GpuPinPoweringNavigation { get; set; }
        public virtual SpecManufacturer ManufacturerNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual SpecVramType VramTypeNavigation { get; set; }
    }
}
