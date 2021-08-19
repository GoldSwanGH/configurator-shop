using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecGpuPinPowering
    {
        public SpecGpuPinPowering()
        {
            CategoryGpus = new HashSet<CategoryGpu>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryGpu> CategoryGpus { get; set; }
    }
}
