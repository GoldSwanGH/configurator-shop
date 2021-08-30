using System.Collections.Generic;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class ConfigurationModel
    {
        public CategoryCpu Cpu { get; set; }

        public CategoryMotherboard Motherboard { get; set; }
        
        public CategoryGpu Gpu { get; set; }
        
        public CategoryPsu Psu { get; set; }
        
        public List<CategoryRam> Ram { get; set; }
        
        public CategoryCase Case { get; set; }
        
        public List<CategoryHdd> Hdd { get; set; }
        
        public List<CategorySsd> Ssd { get; set; }
        
        public CategoryCpuCooler CpuCooler { get; set; }
        
        public List<CategoryCaseFan> CaseFan { get; set; }
        
        public bool SocketAcc { get; set; }
        
        public bool MotherboardFormFactorAcc { get; set; }
        
        public bool CpuPowerAcc { get; set; }
        
        public bool GpuPowerAcc { get; set; }
        
        public bool RamSlotsAcc { get; set; }
        
        public bool Ex25SlotsAcc { get; set; }
        
        public bool Ex35SlotsAcc { get; set; }
        
        public bool CpuCoolerHeightAcc { get; set; }
        
        public bool CpuCoolerTdpAcc { get; set; }
        
        public bool CaseFansAcc { get; set; }
    }
}