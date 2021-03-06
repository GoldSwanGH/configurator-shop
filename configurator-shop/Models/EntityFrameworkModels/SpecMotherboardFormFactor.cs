using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecMotherboardFormFactor
    {
        public SpecMotherboardFormFactor()
        {
            CategoryCases = new HashSet<CategoryCase>();
            CategoryMotherboards = new HashSet<CategoryMotherboard>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryCase> CategoryCases { get; set; }
        public virtual ICollection<CategoryMotherboard> CategoryMotherboards { get; set; }
    }
}
