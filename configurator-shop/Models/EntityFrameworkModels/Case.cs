using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Case
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? FormFactor { get; set; }
        public int? MotherboardFormFactor { get; set; }
        public int? GpuLength { get; set; }
        public int? CpuCoolerHeight { get; set; }
        public int? ExBays25Internal { get; set; }
        public int? ExBays35Internal { get; set; }
        public int? ExBays35External { get; set; }
        public int? ExBays525External { get; set; }
        public int? Fan200Installed { get; set; }
        public int? Fan200Possible { get; set; }
        public int? Fan140Installed { get; set; }
        public int? Fan140Possible { get; set; }
        public int? Fan120Installed { get; set; }
        public int? Fan120Possible { get; set; }
        public int? Fan92Installed { get; set; }
        public int? Fan92Possible { get; set; }
        public int? Fan80Installed { get; set; }
        public int? Fan80Possible { get; set; }
        public int? Thunderbolt { get; set; }
        public int? UsbTypeC { get; set; }
        public int? Usb31 { get; set; }
        public int? Usb30 { get; set; }
        public int? Usb20 { get; set; }
        public int? ESata { get; set; }
        public int? Firewire { get; set; }
        public int? Sound { get; set; }
        public int? Mic { get; set; }
        public int? Window { get; set; }
        public int? Material { get; set; }
        public int? PsuInstalled { get; set; }
        public int? PsuPower { get; set; }
        public int? Color { get; set; }
    }
}
