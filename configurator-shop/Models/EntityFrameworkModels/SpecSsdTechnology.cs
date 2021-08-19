using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecSsdTechnology
    {
        public SpecSsdTechnology()
        {
            CategorySsds = new HashSet<CategorySsd>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategorySsd> CategorySsds { get; set; }
    }
}
