using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using configurator_shop.Interfaces;
using configurator_shop.Models.EntityFrameworkModels;
using configurator_shop.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace configurator_shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly ShopDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IResizer _resizer;
        private readonly ICompresser _compresser;
        private readonly IPictureProcessor _processor;
        private readonly IValueDimensionApplier _dimension;
        
        public ShopController(ILogger<ShopController> logger, ShopDbContext dbContext, IWebHostEnvironment webHostEnvironment, IResizer resizer, ICompresser compresser, IPictureProcessor processor, IValueDimensionApplier dimension)
        {
            _logger = logger;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _resizer = resizer;
            _compresser = compresser;
            _processor = processor;
            _dimension = dimension;
        }

        public IActionResult Categories()
        {
            return View();
        }

        public IActionResult CategoryCpu()
        {
            var model = new CategoryCpuViewModel();
            
            return View(model);
        }
        
        public IActionResult CategoryCaseFan()
        {
            var model = new CategoryCaseFanViewModel();
            
            return View(model);
        }
        
        public IActionResult CategoryCase()
        {
            var model = new CategoryCaseViewModel();
            
            return View(model);
        }
        
        public IActionResult CategoryCpuCooler()
        {
            var model = new CategoryCpuCoolerViewModel();
            
            return View(model);
        }
        
        public IActionResult CategoryGpu()
        {
            var model = new CategoryGpuViewModel();
            
            return View(model);
        }
        
        public IActionResult CategoryHdd()
        {
            var model = new CategoryHddViewModel();
            
            return View(model);
        }
        
        public IActionResult CategoryMotherboard()
        {
            var model = new CategoryMotherboardViewModel();
            
            return View(model);
        }
        
        public IActionResult CategoryPsu()
        {
            var model = new CategoryPsuViewModel();
            
            return View(model);
        }
        
        public IActionResult CategoryRam()
        {
            var model = new CategoryRamViewModel();
            
            return View(model);
        }
        
        public IActionResult CategorySsd()
        {
            var model = new CategorySsdViewModel();
            
            return View(model);
        }
        
        public IActionResult Product()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult AddCpu()
        {
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecCpuFamilies = new SelectList(_dbContext.SpecCpuFamilies, "Id", "Spec");
            ViewBag.SpecGpuChipsets = new SelectList(_dbContext.SpecGpuChipsets, "Id", "Spec");
            ViewBag.SpecCpuPackagings = new SelectList(_dbContext.SpecCpuPackagings, "Id", "Spec");
            ViewBag.SpecRamTechnologies = new SelectList(_dbContext.SpecRamTechnologies, "Id", "Spec");
            ViewBag.SpecCpuSeries = new SelectList(_dbContext.SpecCpuSeries, "Id", "Spec");
            ViewBag.SpecSockets = new SelectList(_dbContext.SpecSockets, "Id", "Spec");
            
            var model = new CpuViewModel();
            var cpuType = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "CPU");
            model.Cpu.Product.TypeId = cpuType.Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddCpu(CpuViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Cpu.Product.Image = fileName;
                }

                var summarySpecs = new List<string>();

                var socketObj = _dbContext.SpecSockets.FirstOrDefault(s => s.Id == model.Cpu.Socket);
                if (socketObj != null) summarySpecs.Add(socketObj.Spec);

                string cores = _dimension.Item(model.Cpu.Cores);
                if (cores != null) summarySpecs.Add("ядер " + cores);

                string threads = _dimension.Item(model.Cpu.Threads);
                if (threads != null) summarySpecs.Add("потоков " + threads);

                string freqBase = _dimension.FreqGHz(model.Cpu.FreqBase);
                if (freqBase != null) summarySpecs.Add("частота " + freqBase);

                string freqBoost = _dimension.FreqGHz(model.Cpu.FreqBoost);
                if (freqBoost != null) summarySpecs.Add("турбо " + freqBoost);

                string techProcess = _dimension.TechNm(model.Cpu.TechProcess);
                if (techProcess != null) summarySpecs.Add(techProcess);

                string tdp = _dimension.EnerW(model.Cpu.Tdp);
                if (tdp != null) summarySpecs.Add(tdp);

                if (model.Cpu.GpuChipset != null)
                {
                    summarySpecs.Add("со встроенной графикой");
                }

                var packagingObj = _dbContext.SpecCpuPackagings.FirstOrDefault(s => s.Id == model.Cpu.Packaging);
                if (packagingObj != null) summarySpecs.Add(packagingObj.Spec);

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.Cpu.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.Cpu.Product.Summary += ", ";
                    }
                }

                _dbContext.CategoryCpus.Add(model.Cpu);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddGpu()
        {
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecGpuConnectorTypes = new SelectList(_dbContext.SpecGpuConnectorTypes, "Id", "Spec");
            ViewBag.SpecGpuChipsets = new SelectList(_dbContext.SpecGpuChipsets, "Id", "Spec");
            ViewBag.SpecDirectxTypes = new SelectList(_dbContext.SpecDirectxTypes, "Id", "Spec");
            ViewBag.SpecGpuPinPowerings = new SelectList(_dbContext.SpecGpuPinPowerings, "Id", "Spec");
            ViewBag.SpecVramTypes = new SelectList(_dbContext.SpecVramTypes, "Id", "Spec");

            var model = new GpuViewModel();
            model.Gpu.Product.TypeId = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "GPU").Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddGpu(GpuViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Gpu.Product.Image = fileName;
                }

                var summarySpecs = new List<string>();
                
                var chipsetObj = _dbContext.SpecGpuChipsets.FirstOrDefault(s => s.Id == model.Gpu.GpuChipset);
                if (chipsetObj != null) summarySpecs.Add(chipsetObj.Spec);
                
                string vram = _dimension.MemoryGb(model.Gpu.Vram);
                if (vram != null) summarySpecs.Add(vram);
                
                string baseFreq = _dimension.FreqMHz(model.Gpu.BaseFreq);
                if (baseFreq != null) summarySpecs.Add("частота " + baseFreq);
                
                string boostFreq = _dimension.FreqMHz(model.Gpu.BoostFreq);
                if (boostFreq != null) summarySpecs.Add("турбо " + boostFreq);

                var directx = _dbContext.SpecDirectxTypes.FirstOrDefault(s => s.Id == model.Gpu.DirectX);
                if (directx != null) summarySpecs.Add("DirectX " + directx.Spec);

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.Gpu.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.Gpu.Product.Summary += ", ";
                    }
                }
                
                _dbContext.CategoryGpus.Add(model.Gpu);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddCase()
        {
            ViewBag.SpecColors = new SelectList(_dbContext.SpecColors, "Id", "Spec");
            ViewBag.SpecCaseFormFactors = new SelectList(_dbContext.SpecCaseFormFactors, "Id", "Spec");
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecCaseMaterials = new SelectList(_dbContext.SpecCaseMaterials, "Id", "Spec");
            ViewBag.SpecMotherboardFormFactors = new SelectList(_dbContext.SpecMotherboardFormFactors, "Id", "Spec");

            var model = new CaseViewModel();
            model.Case.Product.TypeId = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "Case").Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddCase(CaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Case.Product.Image = fileName;
                }

                var summarySpecs = new List<string>();

                var caseFormFactor = _dbContext.SpecCaseFormFactors.FirstOrDefault(s => s.Id == model.Case.FormFactor);
                if (caseFormFactor != null) summarySpecs.Add(caseFormFactor.Spec);
                
                var motherboardFormFactor = _dbContext.SpecMotherboardFormFactors.FirstOrDefault(s => s.Id == model.Case.MotherboardFormFactor);
                if (motherboardFormFactor != null) summarySpecs.Add(motherboardFormFactor.Spec);
                
                var color = _dbContext.SpecColors.FirstOrDefault(s => s.Id == model.Case.Color);
                if (color != null) summarySpecs.Add(color.Spec);

                if (model.Case.Window)
                {
                    summarySpecs.Add("с окном");
                }

                if (model.Case.PsuInstalled)
                {
                    string psu = "с блоком питания";
                    string psuPower = _dimension.EnerW(model.Case.PsuPower);
                    if (psuPower != null) psu += " " + psuPower;
                    summarySpecs.Add(psu);
                }

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.Case.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.Case.Product.Summary += ", ";
                    }
                }
                
                _dbContext.CategoryCases.Add(model.Case);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddCaseFan()
        {
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecFanSizes = new SelectList(_dbContext.SpecFanSizes, "Id", "Spec");
            
            var model = new CaseFanViewModel();
            model.CaseFan.Product.TypeId = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "CaseFan").Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddCaseFan(CaseFanViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.CaseFan.Product.Image = fileName;
                }
                
                var summarySpecs = new List<string>();

                var fanSize = _dbContext.SpecFanSizes.FirstOrDefault(s => s.Id == model.CaseFan.FanSize);
                if (fanSize != null) summarySpecs.Add(fanSize.Spec);
                
                string speed = _dimension.SpeedSpinsPreMinute(model.CaseFan.Speed);
                if (speed != null) summarySpecs.Add(speed);
                
                string noise = _dimension.NoiseDb(model.CaseFan.Noise);
                if (noise != null) summarySpecs.Add(noise);

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.CaseFan.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.CaseFan.Product.Summary += ", ";
                    }
                }

                _dbContext.CategoryCaseFans.Add(model.CaseFan);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddCpuCooler()
        {
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecCoolerTypes = new SelectList(_dbContext.SpecCoolerTypes, "Id", "Spec");
            ViewBag.SpecSockets = new SelectList(_dbContext.SpecSockets, "Id", "Spec");
            
            var model = new CpuCoolerViewModel();
            model.CpuCooler.Product.TypeId = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "CpuCooler").Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddCpuCooler(CpuCoolerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.CpuCooler.Product.Image = fileName;
                }
                
                var summarySpecs = new List<string>();

                var coolerType = _dbContext.SpecCoolerTypes.FirstOrDefault(s => s.Id == model.CpuCooler.CoolerType);
                if (coolerType != null) summarySpecs.Add(coolerType.Spec);
                
                string tdp = _dimension.EnerW(model.CpuCooler.Tdp);
                if (tdp != null) summarySpecs.Add(tdp);

                string noise = _dimension.NoiseDb(model.CpuCooler.Noise);
                if (noise != null) summarySpecs.Add(noise);

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.CpuCooler.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.CpuCooler.Product.Summary += ", ";
                    }
                }

                _dbContext.CategoryCpuCoolers.Add(model.CpuCooler);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddHdd()
        {
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecHddFormFactors = new SelectList(_dbContext.SpecHddFormFactors, "Id", "Spec");
            ViewBag.SpecHddInterfaces = new SelectList(_dbContext.SpecHddInterfaces, "Id", "Spec");
            
            var model = new HddViewModel();
            model.Hdd.Product.TypeId = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "HDD").Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddHdd(HddViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Hdd.Product.Image = fileName;
                }

                var summarySpecs = new List<string>();

                if (model.Hdd.Capacity < 1024000)
                {
                    string capacity = _dimension.MemoryGb(model.Hdd.Capacity);
                    if (capacity != null) summarySpecs.Add(capacity);
                }
                else
                {
                    string capacity = _dimension.MemoryTb(model.Hdd.Capacity);
                    if (capacity != null) summarySpecs.Add(capacity);
                }
                
                
                var formFactor = _dbContext.SpecHddFormFactors.FirstOrDefault(s => s.Id == model.Hdd.FormFactor);
                if (formFactor != null) summarySpecs.Add(formFactor.Spec);
                
                var hddInterface = _dbContext.SpecHddInterfaces.FirstOrDefault(s => s.Id == model.Hdd.Interface);
                if (hddInterface != null) summarySpecs.Add(hddInterface.Spec);

                string speed = _dimension.SpeedSpinsPreMinute(model.Hdd.SpindleSpeed);
                if (speed != null) summarySpecs.Add(speed);
                
                string cache = _dimension.MemoryMb(model.Hdd.Cache);
                if (cache != null) summarySpecs.Add(cache);

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.Hdd.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.Hdd.Product.Summary += ", ";
                    }
                }
                
                _dbContext.CategoryHdds.Add(model.Hdd);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddMotherboard()
        {
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecMotherboardChipsets = new SelectList(_dbContext.SpecMotherboardChipsets, "Id", "Spec");
            ViewBag.SpecCpuPinPowerings = new SelectList(_dbContext.SpecCpuPinPowerings, "Id", "Spec");
            ViewBag.SpecMotherboardFormFactors = new SelectList(_dbContext.SpecMotherboardFormFactors, "Id", "Spec");
            ViewBag.SpecPciex16versions = new SelectList(_dbContext.SpecPciex16versions, "Id", "Spec");
            ViewBag.SpecRamTechnologies = new SelectList(_dbContext.SpecRamTechnologies, "Id", "Spec");
            ViewBag.SpecRamTypes = new SelectList(_dbContext.SpecRamTypes, "Id", "Spec");
            ViewBag.SpecSockets = new SelectList(_dbContext.SpecSockets, "Id", "Spec");
            
            var model = new MotherboardViewModel();
            model.Motherboard.Product.TypeId = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "Motherboard").Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddMotherboard(MotherboardViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Motherboard.Product.Image = fileName;
                }

                var summarySpecs = new List<string>();

                var socket = _dbContext.SpecSockets.FirstOrDefault(s => s.Id == model.Motherboard.Socket);
                if (socket != null) summarySpecs.Add(socket.Spec);
                
                var formFactor = _dbContext.SpecMotherboardFormFactors.FirstOrDefault(s => s.Id == model.Motherboard.FormFactor);
                if (formFactor != null) summarySpecs.Add(formFactor.Spec);
                
                var chipset = _dbContext.SpecMotherboardChipsets.FirstOrDefault(s => s.Id == model.Motherboard.Chipset);
                if (chipset != null) summarySpecs.Add(chipset.Spec);
                
                var ramTech = _dbContext.SpecRamTechnologies.FirstOrDefault(s => s.Id == model.Motherboard.RamTechnology);
                if (ramTech != null) summarySpecs.Add(ramTech.Spec);

                string slots = _dimension.Item(model.Motherboard.RamSlots);
                if (slots != null) summarySpecs.Add("слотов ОЗУ " + slots);
                
                var pci = _dbContext.SpecPciex16versions.FirstOrDefault(s => s.Id == model.Motherboard.Pciex16version);
                if (pci != null) summarySpecs.Add("PCIe x16 v" + pci.Spec);

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.Motherboard.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.Motherboard.Product.Summary += ", ";
                    }
                }
                
                _dbContext.CategoryMotherboards.Add(model.Motherboard);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddPsu()
        {
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecPsuFormFactors = new SelectList(_dbContext.SpecPsuFormFactors, "Id", "Spec");
            ViewBag.SpecPsuPlusTypes = new SelectList(_dbContext.SpecPsuPlusTypes, "Id", "Spec");
            
            var model = new PsuViewModel();
            model.Psu.Product.TypeId = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "PSU").Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddPsu(PsuViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Psu.Product.Image = fileName;
                }
                
                var summarySpecs = new List<string>();
                
                string power = _dimension.EnerW(model.Psu.Power);
                if (power != null) summarySpecs.Add(power);

                var formFactor = _dbContext.SpecPsuFormFactors.FirstOrDefault(s => s.Id == model.Psu.FormFactor);
                if (formFactor != null) summarySpecs.Add(formFactor.Spec);
                
                var plus = _dbContext.SpecPsuPlusTypes.FirstOrDefault(s => s.Id == model.Psu.Plus);
                if (plus != null) summarySpecs.Add(plus.Spec);
                
                if (model.Psu.Pfc)
                {
                    summarySpecs.Add("PFC");
                }

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.Psu.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.Psu.Product.Summary += ", ";
                    }
                }

                _dbContext.CategoryPsus.Add(model.Psu);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddRam()
        {
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecRamTechnologies = new SelectList(_dbContext.SpecRamTechnologies, "Id", "Spec");
            ViewBag.SpecRamTypes = new SelectList(_dbContext.SpecRamTypes, "Id", "Spec");
            
            var model = new RamViewModel();
            model.Ram.Product.TypeId = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "RAM").Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddRam(RamViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Ram.Product.Image = fileName;
                }

                var summarySpecs = new List<string>();

                var tech = _dbContext.SpecRamTechnologies.FirstOrDefault(s => s.Id == model.Ram.RamTechnology);
                if (tech != null) summarySpecs.Add(tech.Spec);
                
                var type = _dbContext.SpecRamTypes.FirstOrDefault(s => s.Id == model.Ram.RamType);
                if (type != null) summarySpecs.Add(type.Spec);
                
                string capacity = _dimension.MemoryGb(model.Ram.CapacityPerModule);
                if (capacity != null) summarySpecs.Add(capacity);
                
                string modules = _dimension.Item(model.Ram.Modules);
                if (modules != null) summarySpecs.Add(modules);
                
                string freq = _dimension.FreqMHz(model.Ram.Frequency);
                if (freq != null) summarySpecs.Add(freq);

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.Ram.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.Ram.Product.Summary += ", ";
                    }
                }
                
                _dbContext.CategoryRams.Add(model.Ram);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddSsd()
        {
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecSsdInterfaces = new SelectList(_dbContext.SpecSsdInterfaces, "Id", "Spec");
            ViewBag.SpecSddFormFactors = new SelectList(_dbContext.SpecSddFormFactors, "Id", "Spec");
            ViewBag.SpecSsdTechnologies = new SelectList(_dbContext.SpecSsdTechnologies, "Id", "Spec");
            
            var model = new SsdViewModel();
            model.Ssd.Product.TypeId = _dbContext.ProductTypes.FirstOrDefault(t => t.Type == "SSD").Id;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddSsd(SsdViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Ssd.Product.Image = fileName;
                }
                
                var summarySpecs = new List<string>();
                
                if (model.Ssd.Capacity < 1024000)
                {
                    string capacity = _dimension.MemoryGb(model.Ssd.Capacity);
                    if (capacity != null) summarySpecs.Add(capacity);
                }
                else
                {
                    string capacity = _dimension.MemoryTb(model.Ssd.Capacity);
                    if (capacity != null) summarySpecs.Add(capacity);
                }

                var formFactor = _dbContext.SpecSddFormFactors.FirstOrDefault(s => s.Id == model.Ssd.FormFactor);
                if (formFactor != null) summarySpecs.Add(formFactor.Spec);
                
                var ssdInterface = _dbContext.SpecSsdInterfaces.FirstOrDefault(s => s.Id == model.Ssd.Interface);
                if (ssdInterface != null) summarySpecs.Add(ssdInterface.Spec);
                
                string read = _dimension.MemoryMbPerSecond(model.Ssd.ReadSpeed);
                if (read != null) summarySpecs.Add("чтение " + read);
                
                string write = _dimension.MemoryMbPerSecond(model.Ssd.WriteSpeed);
                if (write != null) summarySpecs.Add("запись " + write);

                for (int i = 0; i < summarySpecs.Count; i++)
                {
                    model.Ssd.Product.Summary += summarySpecs[i];
                    if (i != (summarySpecs.Count - 1))
                    {
                        model.Ssd.Product.Summary += ", ";
                    }
                }

                _dbContext.CategorySsds.Add(model.Ssd);
                _dbContext.SaveChanges();
                return RedirectToAction("AddProduct", "Shop");
            }
            
            return View(model);
        }
    }
}