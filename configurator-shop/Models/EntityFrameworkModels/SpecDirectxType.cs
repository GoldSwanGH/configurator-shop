using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecDirectxType
    {
        public SpecDirectxType()
        {
            CategoryGpus = new HashSet<CategoryGpu>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryGpu> CategoryGpus { get; set; }
    }
}
