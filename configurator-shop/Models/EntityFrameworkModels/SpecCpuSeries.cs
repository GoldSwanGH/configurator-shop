using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecCpuSeries
    {
        public SpecCpuSeries()
        {
            CategoryCpus = new HashSet<CategoryCpu>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryCpu> CategoryCpus { get; set; }
    }
}
