using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecHddInterface
    {
        public SpecHddInterface()
        {
            CategoryHdds = new HashSet<CategoryHdd>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryHdd> CategoryHdds { get; set; }
    }
}
