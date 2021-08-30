using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ViewHdd
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Manufacturer { get; set; }
        public string FormFactor { get; set; }
        public string Interface { get; set; }
        public int? SpindleSpeed { get; set; }
        public int? Capacity { get; set; }
        public int? Cache { get; set; }
    }
}
