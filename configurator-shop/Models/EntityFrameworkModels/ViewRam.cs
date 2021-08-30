using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ViewRam
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Manufacturer { get; set; }
        public string RamType { get; set; }
        public int? Modules { get; set; }
        public int? Frequency { get; set; }
        public string RamTechnology { get; set; }
        public int? CapacityPerModule { get; set; }
    }
}
