using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ViewPsu
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Manufacturer { get; set; }
        public int? Power { get; set; }
        public string FormFactor { get; set; }
        public bool Pfc { get; set; }
        public int? Sata { get; set; }
        public int? Molex { get; set; }
        public int? Pciex24 { get; set; }
        public int? Pciex8 { get; set; }
        public int? Pciex6 { get; set; }
        public int? Pciex4 { get; set; }
        public int? Pciex2 { get; set; }
        public string Plus { get; set; }
    }
}
