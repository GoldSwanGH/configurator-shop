﻿using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecCpuFamily
    {
        public SpecCpuFamily()
        {
            CategoryCpus = new HashSet<CategoryCpu>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryCpu> CategoryCpus { get; set; }
    }
}
