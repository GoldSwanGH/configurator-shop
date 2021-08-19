using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecRamType
    {
        public SpecRamType()
        {
            CategoryRams = new HashSet<CategoryRam>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryRam> CategoryRams { get; set; }
    }
}
