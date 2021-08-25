using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class CategorySsd
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? FormFactor { get; set; }
        public int? Interface { get; set; }
        public int? Capacity { get; set; }
        public int? Technology { get; set; }
        public bool Nvme { get; set; }
        public int? ReadSpeed { get; set; }
        public int? WriteSpeed { get; set; }
        public bool HardwareEncryption { get; set; }

        public virtual SpecSddFormFactor FormFactorNavigation { get; set; }
        public virtual SpecSsdInterface InterfaceNavigation { get; set; }
        public virtual SpecManufacturer ManufacturerNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual SpecSsdTechnology TechnologyNavigation { get; set; }
    }
}
