using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecPsuFormFactor
    {
        public SpecPsuFormFactor()
        {
            CategoryPsus = new HashSet<CategoryPsu>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryPsu> CategoryPsus { get; set; }
    }
}
