using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecPciex16version
    {
        public SpecPciex16version()
        {
            CategoryMotherboards = new HashSet<CategoryMotherboard>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryMotherboard> CategoryMotherboards { get; set; }
    }
}
