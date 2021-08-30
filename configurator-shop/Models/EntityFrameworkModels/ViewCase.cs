using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ViewCase
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Manufacturer { get; set; }
        public string FormFactor { get; set; }
        public string MotherboardFormFactor { get; set; }
        public int? GpuMaxLength { get; set; }
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
        public bool Window { get; set; }
        public string Material { get; set; }
        public bool PsuInstalled { get; set; }
        public int? PsuPower { get; set; }
        public string Color { get; set; }
    }
}
