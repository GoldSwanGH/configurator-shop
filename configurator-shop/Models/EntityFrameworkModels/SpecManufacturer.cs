using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class SpecManufacturer
    {
        public SpecManufacturer()
        {
            CategoryCaseFans = new HashSet<CategoryCaseFan>();
            CategoryCases = new HashSet<CategoryCase>();
            CategoryCpuCoolers = new HashSet<CategoryCpuCooler>();
            CategoryCpus = new HashSet<CategoryCpu>();
            CategoryGpuGpuManufacturerNavigations = new HashSet<CategoryGpu>();
            CategoryGpuManufacturerNavigations = new HashSet<CategoryGpu>();
            CategoryHdds = new HashSet<CategoryHdd>();
            CategoryMotherboards = new HashSet<CategoryMotherboard>();
            CategoryPsus = new HashSet<CategoryPsu>();
            CategoryRams = new HashSet<CategoryRam>();
            CategorySsds = new HashSet<CategorySsd>();
        }

        public int Id { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<CategoryCaseFan> CategoryCaseFans { get; set; }
        public virtual ICollection<CategoryCase> CategoryCases { get; set; }
        public virtual ICollection<CategoryCpuCooler> CategoryCpuCoolers { get; set; }
        public virtual ICollection<CategoryCpu> CategoryCpus { get; set; }
        public virtual ICollection<CategoryGpu> CategoryGpuGpuManufacturerNavigations { get; set; }
        public virtual ICollection<CategoryGpu> CategoryGpuManufacturerNavigations { get; set; }
        public virtual ICollection<CategoryHdd> CategoryHdds { get; set; }
        public virtual ICollection<CategoryMotherboard> CategoryMotherboards { get; set; }
        public virtual ICollection<CategoryPsu> CategoryPsus { get; set; }
        public virtual ICollection<CategoryRam> CategoryRams { get; set; }
        public virtual ICollection<CategorySsd> CategorySsds { get; set; }
    }
}
