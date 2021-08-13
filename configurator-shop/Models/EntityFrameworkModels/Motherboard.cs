using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Motherboard
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? Socket { get; set; }
        public int? FormFactor { get; set; }
        public int? Chipset { get; set; }
        public int? RamType { get; set; }
        public int? RamSockets { get; set; }
        public int? RamMax { get; set; }
        public int? Pciex16 { get; set; }
        public int? Pciex16version { get; set; }
        public int? Pciex4 { get; set; }
        public int? Pciex1 { get; set; }
        public int? Pci { get; set; }
        public int? Sata3 { get; set; }
        public int? Sata3raid { get; set; }
        public int? Sata2 { get; set; }
        public int? Sata2raid { get; set; }
        public int? M2 { get; set; }
        public int? Thunderbolt { get; set; }
        public int? UsbTypeC { get; set; }
        public int? Usb31 { get; set; }
        public int? Usb30 { get; set; }
        public int? Usb20 { get; set; }
        public int? Ethernet { get; set; }
        public int? WiFi { get; set; }
        public int? Sli { get; set; }
        public int? Crossfire { get; set; }
    }
}
