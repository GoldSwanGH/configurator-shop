using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class CategoryMotherboard
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? Socket { get; set; }
        public int? FormFactor { get; set; }
        public int? Chipset { get; set; }
        public int? RamTechnology { get; set; }
        public int? RamSlots { get; set; }
        public int? RamMaxTotalSize { get; set; }
        public int? Pciex16 { get; set; }
        public int? Pciex16version { get; set; }
        public int? Pciex4 { get; set; }
        public int? Pciex1 { get; set; }
        public int? Pci { get; set; }
        public int? Sata3 { get; set; }
        public bool? Sata3raid { get; set; }
        public int? Sata2 { get; set; }
        public bool? Sata2raid { get; set; }
        public int? M2 { get; set; }
        public int? Thunderbolt { get; set; }
        public int? UsbTypeC { get; set; }
        public int? Usb31 { get; set; }
        public int? Usb30 { get; set; }
        public int? Usb20 { get; set; }
        public bool? WiFi { get; set; }
        public bool? Sli { get; set; }
        public bool? Crossfire { get; set; }
        public int? CpuPinPowering { get; set; }

        public virtual SpecMotherboardChipset ChipsetNavigation { get; set; }
        public virtual SpecCpuPinPowering CpuPinPoweringNavigation { get; set; }
        public virtual SpecMotherboardFormFactor FormFactorNavigation { get; set; }
        public virtual SpecManufacturer ManufacturerNavigation { get; set; }
        public virtual SpecPciex16version Pciex16versionNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual SpecRamTechnology RamTechnologyNavigation { get; set; }
        public virtual SpecSocket SocketNavigation { get; set; }
    }
}
