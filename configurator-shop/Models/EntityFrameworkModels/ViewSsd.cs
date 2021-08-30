using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ViewSsd
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Manufacturer { get; set; }
        public string SsdFormFactor { get; set; }
        public string SsdInterface { get; set; }
        public int? Capacity { get; set; }
        public string SsdTechnology { get; set; }
        public bool Nvme { get; set; }
        public int? ReadSpeed { get; set; }
        public int? WriteSpeed { get; set; }
        public bool HardwareEncryption { get; set; }
    }
}
