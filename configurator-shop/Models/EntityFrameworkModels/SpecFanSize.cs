using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecFanSize
    {
        public SpecFanSize()
        {
            CategoryCaseFans = new HashSet<CategoryCaseFan>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryCaseFan> CategoryCaseFans { get; set; }
    }
}
