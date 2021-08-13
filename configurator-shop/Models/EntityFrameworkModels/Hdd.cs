using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Hdd
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? FormFactor { get; set; }
        public int? Interface { get; set; }
        public int? SpindleSpeed { get; set; }
        public int? Capacity { get; set; }
        public int? Cache { get; set; }

        public virtual Product Product { get; set; }
    }
}
