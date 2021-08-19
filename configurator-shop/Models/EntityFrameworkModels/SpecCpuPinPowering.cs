using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecCpuPinPowering
    {
        public SpecCpuPinPowering()
        {
            CategoryMotherboards = new HashSet<CategoryMotherboard>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryMotherboard> CategoryMotherboards { get; set; }
    }
}
