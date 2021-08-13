using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Psu
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? Power { get; set; }
        public int? FormFactor { get; set; }
        public int? Pfc { get; set; }
        public int? Sata { get; set; }
        public int? Molex { get; set; }
        public int? Pciex8 { get; set; }
        public int? Pciex6 { get; set; }
        public int? Plus { get; set; }

        public virtual Product Product { get; set; }
    }
}
