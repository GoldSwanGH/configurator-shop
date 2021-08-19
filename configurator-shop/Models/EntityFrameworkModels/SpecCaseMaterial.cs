using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecCaseMaterial
    {
        public SpecCaseMaterial()
        {
            CategoryCases = new HashSet<CategoryCase>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryCase> CategoryCases { get; set; }
    }
}
