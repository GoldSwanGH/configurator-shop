using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecSocket
    {
        public SpecSocket()
        {
            CategoryCpuCoolers = new HashSet<CategoryCpuCooler>();
            CategoryCpus = new HashSet<CategoryCpu>();
            CategoryMotherboards = new HashSet<CategoryMotherboard>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryCpuCooler> CategoryCpuCoolers { get; set; }
        public virtual ICollection<CategoryCpu> CategoryCpus { get; set; }
        public virtual ICollection<CategoryMotherboard> CategoryMotherboards { get; set; }
    }
}
