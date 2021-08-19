using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class CategoryPsu
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? Power { get; set; }
        public int? FormFactor { get; set; }
        public bool? Pfc { get; set; }
        public int? Sata { get; set; }
        public int? Molex { get; set; }
        public int? Pciex8 { get; set; }
        public int? Pciex6 { get; set; }
        public int? Plus { get; set; }

        public virtual SpecPsuFormFactor FormFactorNavigation { get; set; }
        public virtual SpecManufacturer ManufacturerNavigation { get; set; }
        public virtual SpecPsuPlusType PlusNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
