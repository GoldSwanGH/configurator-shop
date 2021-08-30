using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ViewMotherboard
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Manufacturer { get; set; }
        public string Socket { get; set; }
        public string FormFactor { get; set; }
        public string Chipset { get; set; }
        public string RamTechnology { get; set; }
        public int? RamSlots { get; set; }
        public int? RamMaxTotalSize { get; set; }
        public int? Pciex16 { get; set; }
        public string Pciex16version { get; set; }
        public int? Pciex4 { get; set; }
        public int? Pciex1 { get; set; }
        public int? Pci { get; set; }
        public int? Sata3 { get; set; }
        public bool Sata3raid { get; set; }
        public int? Sata2 { get; set; }
        public bool Sata2raid { get; set; }
        public int? M2 { get; set; }
        public int? Thunderbolt { get; set; }
        public int? UsbTypeC { get; set; }
        public int? Usb31 { get; set; }
        public int? Usb30 { get; set; }
        public int? Usb20 { get; set; }
        public bool WiFi { get; set; }
        public bool Sli { get; set; }
        public bool Crossfire { get; set; }
        public string CpuPinPowering { get; set; }
        public string RamTypes { get; set; }
    }
}
