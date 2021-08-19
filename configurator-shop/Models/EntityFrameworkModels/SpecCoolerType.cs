using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecCoolerType
    {
        public SpecCoolerType()
        {
            CategoryCpuCoolers = new HashSet<CategoryCpuCooler>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryCpuCooler> CategoryCpuCoolers { get; set; }
    }
}
