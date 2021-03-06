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
using Humanizer;

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
            
            model.Products.AddRange(_dbContext.CategoryCpus.Include(c => c.Product).ToList());
            
            ViewBag.SpecManufacturers = new SelectList(_dbContext.SpecManufacturers, "Id", "Spec");
            ViewBag.SpecCpuFamilies = new SelectList(_dbContext.SpecCpuFamilies, "Id", "Spec");
            ViewBag.SpecGpuChipsets = new SelectList(_dbContext.SpecGpuChipsets, "Id", "Spec");
            ViewBag.SpecCpuPackagings = new SelectList(_dbContext.SpecCpuPackagings, "Id", "Spec");
            ViewBag.SpecRamTechnologies = new SelectList(_dbContext.SpecRamTechnologies, "Id", "Spec");
            ViewBag.SpecCpuSeries = new SelectList(_dbContext.SpecCpuSeries, "Id", "Spec");
            ViewBag.SpecSockets = new SelectList(_dbContext.SpecSockets, "Id", "Spec");
            
            /*
            var list = new List<string>();
            var category = new CategoryCpu();
            
            foreach (var propertyInfo in category.GetType().GetProperties())
            {
                string name = "Spec" + propertyInfo.Name;

                name = name.Pluralize();
                list.Add(name);
            }
            
            var category = new CategoryCpu();
            var collection = new List<string>();
            var ignored = new[] {"Name", "Description", "ProductID", "Manufacturer", "Price"};

            foreach (var propertyInfo in category.GetType().GetProperties())
            {
                foreach (var item in ignored) 
                {
                    if (propertyInfo.Name != item) {collection.Add("Spec" + propertyInfo.Name.Pluralize());}
                }
            }

            var dictionary = new Dictionary<string, SelectList>();
            
            foreach (var item in collection)
            {
                try
                {
                    dictionary.Add(item, new SelectList((DbSet<SpecCpuFamily>)_dbContext.GetType().GetProperty(item).GetValue(_dbContext), "Id", "Spec"));
                }
                catch
                {
                    continue;
                }
            }
            
            //ViewBag.NamesList = list;*/
            
            return View(model);
        }
        
        public IActionResult CategoryCaseFan()
        {
            var model = new CategoryCaseFanViewModel();
            
            model.Products.AddRange(_dbContext.CategoryCaseFans.Include(c => c.Product).ToList());
            
            return View(model);
        }
        
        public IActionResult CategoryCase()
        {
            var model = new CategoryCaseViewModel();
            
            model.Products.AddRange(_dbContext.CategoryCases.Include(c => c.Product).ToList());
            
            return View(model);
        }
        
        public IActionResult CategoryCpuCooler()
        {
            var model = new CategoryCpuCoolerViewModel();
            
            model.Products.AddRange(_dbContext.CategoryCpuCoolers.Include(c => c.Product).ToList());
            
            return View(model);
        }
        
        public IActionResult CategoryGpu()
        {
            var model = new CategoryGpuViewModel();
            
            model.Products.AddRange(_dbContext.CategoryGpus.Include(c => c.Product).ToList());
            
            return View(model);
        }
        
        public IActionResult CategoryHdd()
        {
            var model = new CategoryHddViewModel();
            
            model.Products.AddRange(_dbContext.CategoryHdds.Include(c => c.Product).ToList());
            
            return View(model);
        }
        
        public IActionResult CategoryMotherboard()
        {
            var model = new CategoryMotherboardViewModel();
            
            model.Products.AddRange(_dbContext.CategoryMotherboards.Include(c => c.Product).ToList());
            
            return View(model);
        }
        
        public IActionResult CategoryPsu()
        {
            var model = new CategoryPsuViewModel();
            
            model.Products.AddRange(_dbContext.CategoryPsus.Include(c => c.Product).ToList());
            
            return View(model);
        }
        
        public IActionResult CategoryRam()
        {
            var model = new CategoryRamViewModel();
            
            model.Products.AddRange(_dbContext.CategoryRams.Include(c => c.Product).ToList());
            
            return View(model);
        }
        
        public IActionResult CategorySsd()
        {
            var model = new CategorySsdViewModel();
            
            model.Products.AddRange(_dbContext.CategorySsds.Include(c => c.Product).ToList());
            
            return View(model);
        }
        
        public IActionResult Product(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            var model = new ProductViewModel();
            if (product != null)
            {
                model.Product = product;
                
                switch (product.TypeId)
                {
                    case 1:
                        var cpu = _dbContext.ViewCpus.FirstOrDefault(v => v.Id == product.Id);
                        if (cpu != null)
                        {
                            if (cpu.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", cpu.Manufacturer));
                            }

                            if (cpu.Family != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Семейство процессоров", cpu.Family));
                            }

                            if (cpu.Series != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Серия", cpu.Series));
                            }

                            if (cpu.Socket != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Сокет", cpu.Socket));
                            }
                            
                            if (cpu.Cores != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество ядер", _dimension.Item(cpu.Cores)));
                            }
                            
                            if (cpu.Threads != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество потоков", _dimension.Item(cpu.Threads)));
                            }
                            
                            if (cpu.FreqBase != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Базовая частота", _dimension.FreqGHz(cpu.FreqBase)));
                            }
                            
                            if (cpu.FreqBoost != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Турбо частота", _dimension.FreqGHz(cpu.FreqBoost)));
                            }
                            
                            if (cpu.TechProcess != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Техпроцесс", _dimension.TechNm(cpu.Threads)));
                            }
                            
                            if (cpu.UnlockedMultiplier)
                            {
                                model.Specs.Add(new Tuple<string, string>("Разблокированный множитель", "да"));
                            }

                            if (cpu.RamTechnology != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Тип памяти", cpu.RamTechnology));
                            }
                            
                            if (cpu.GpuChipset != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Встроенное графическое ядро", cpu.GpuChipset));
                            }
                            
                            if (cpu.Tdp != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Тепловыделение", _dimension.EnerW(cpu.Tdp)));
                            }
                            
                            if (cpu.Packaging != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Тип поставки", cpu.Packaging));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    case 2:
                        var gpu = _dbContext.ViewGpus.FirstOrDefault(v => v.Id == product.Id);
                        if (gpu != null)
                        {
                            if (gpu.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", gpu.Manufacturer));
                            }

                            if (gpu.GpuChipset != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Графическое ядро", gpu.GpuChipset));
                            }
                            
                            if (gpu.GpuManufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель графического ядра", gpu.GpuManufacturer));
                            }

                            if (gpu.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель видеокарты", gpu.Manufacturer));
                            }

                            if (gpu.Vram != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество видеопамяти", _dimension.MemoryGb(gpu.Vram)));
                            }
                            
                            if (gpu.VramType != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Тип видеопамяти", gpu.VramType));
                            }
                            
                            if (gpu.BaseFreq != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Базовая частота ядра", _dimension.FreqMHz(gpu.BaseFreq)));
                            }
                            
                            if (gpu.BoostFreq != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Турбо частота ядра", _dimension.FreqMHz(gpu.BoostFreq)));
                            }
                            
                            if (gpu.BusWidth != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Шина видеопамяти", _dimension.MemoryBit(gpu.BusWidth)));
                            }
                            
                            if (gpu.DirectX != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Версия DirectX", gpu.DirectX));
                            }

                            if (gpu.Hdmi != null && gpu.Hdmi != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество разъемов HDMI", _dimension.Item(gpu.Hdmi)));
                            }
                            
                            if (gpu.DisplayPort != null  && gpu.DisplayPort != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество разъемов DisplayPort", _dimension.Item(gpu.DisplayPort)));
                            }
                            
                            if (gpu.MiniDisplayPort != null && gpu.MiniDisplayPort != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество разъемов MiniDisplayPort", _dimension.Item(gpu.MiniDisplayPort)));
                            }
                            
                            if (gpu.Vga != null && gpu.Vga != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество разъемов VGA", _dimension.Item(gpu.Vga)));
                            }
                            
                            if (gpu.GpuPinPowering != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Дополнительное питание видеокарты", gpu.GpuPinPowering));
                            }
                            
                            if (gpu.Connector != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Интерфейс", gpu.Connector));
                            }
                            
                            if (gpu.Size != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Длина видеокарты", _dimension.LenMm(gpu.Size)));
                            }
                            
                            if (gpu.RecommendedPower != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Рекомендуемая мощность БП", _dimension.EnerW(gpu.RecommendedPower)));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    case 3:
                        var pcCase = _dbContext.ViewCases.FirstOrDefault(v => v.Id == product.Id);
                        if (pcCase != null)
                        {
                            if (pcCase.Color != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Цвет", pcCase.Color));
                            }
                            
                            if (pcCase.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", pcCase.Manufacturer));
                            }

                            if (pcCase.FormFactor != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Форм-фактор", pcCase.FormFactor));
                            }

                            if (pcCase.MotherboardFormFactor != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Форм-фактор материнской платы", pcCase.MotherboardFormFactor));
                            }

                            if (pcCase.GpuMaxLength != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Максимальная длина видеокарты", _dimension.LenMm(pcCase.GpuMaxLength)));
                            }
                            
                            if (pcCase.CpuCoolerHeight != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Максимальная высота процессорного кулера", _dimension.LenMm(pcCase.CpuCoolerHeight)));
                            }
                            
                            if (pcCase.ExBays25Internal != null && pcCase.ExBays25Internal != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Внутренние 2\"5 разъемы", _dimension.Item(pcCase.ExBays25Internal)));
                            }
                            
                            if (pcCase.ExBays35Internal != null && pcCase.ExBays35Internal != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Внутренние 3\"5 разъемы", _dimension.Item(pcCase.ExBays35Internal)));
                            }
                            
                            if (pcCase.ExBays35External != null && pcCase.ExBays35External != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Внешние 3\"5 разъемы", _dimension.Item(pcCase.ExBays35External)));
                            }
                            
                            if (pcCase.ExBays525External != null && pcCase.ExBays525External != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Внешние 5\"25 разъемы", _dimension.Item(pcCase.ExBays525External)));
                            }
                            
                            if (pcCase.Fan200Possible != null && pcCase.Fan200Possible != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Разъемов под вентилятор 200x200 мм", _dimension.Item(pcCase.Fan200Possible)));
                            }
                            
                            if (pcCase.Fan200Installed != null && pcCase.Fan200Installed != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("В комлекте вентиляторов 200x200 мм", _dimension.Item(pcCase.Fan200Possible)));
                            }
                            
                            if (pcCase.Fan140Possible != null && pcCase.Fan140Possible != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Разъемов под вентилятор 140x140 мм", _dimension.Item(pcCase.Fan140Possible)));
                            }
                            
                            if (pcCase.Fan140Installed != null && pcCase.Fan140Installed != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("В комлекте вентиляторов 140x140 мм", _dimension.Item(pcCase.Fan140Possible)));
                            }
                            
                            if (pcCase.Fan120Possible != null && pcCase.Fan120Possible != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Разъемов под вентилятор 120x120 мм", _dimension.Item(pcCase.Fan120Possible)));
                            }
                            
                            if (pcCase.Fan120Installed != null && pcCase.Fan120Installed != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("В комлекте вентиляторов 120x120 мм", _dimension.Item(pcCase.Fan120Possible)));
                            }
                            
                            if (pcCase.Fan92Possible != null && pcCase.Fan92Possible != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Разъемов под вентилятор 92x92 мм", _dimension.Item(pcCase.Fan92Possible)));
                            }
                            
                            if (pcCase.Fan92Installed != null && pcCase.Fan92Installed != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("В комлекте вентиляторов 92x92 мм", _dimension.Item(pcCase.Fan92Possible)));
                            }
                            
                            if (pcCase.Fan80Possible != null && pcCase.Fan80Possible != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Разъемов под вентилятор 80x80 мм", _dimension.Item(pcCase.Fan80Possible)));
                            }
                            
                            if (pcCase.Fan80Installed != null && pcCase.Fan80Installed != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("В комлекте вентиляторов 80x80 мм", _dimension.Item(pcCase.Fan80Possible)));
                            }
                            
                            if (pcCase.Thunderbolt != null && pcCase.Thunderbolt != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Thunderbolt", _dimension.Item(pcCase.Thunderbolt)));
                            }
                            
                            if (pcCase.UsbTypeC != null && pcCase.UsbTypeC != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("USB Type-C", _dimension.Item(pcCase.UsbTypeC)));
                            }
                            
                            if (pcCase.Usb31 != null && pcCase.Usb31 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("USB 3.1", _dimension.Item(pcCase.Usb31)));
                            }
                            
                            if (pcCase.Usb30 != null && pcCase.Usb30 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("USB 3.0", _dimension.Item(pcCase.Usb30)));
                            }
                            
                            if (pcCase.Usb20 != null && pcCase.Usb20 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("USB 2.0", _dimension.Item(pcCase.Usb20)));
                            }
                            
                            if (pcCase.ESata != null && pcCase.ESata != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("ESata", _dimension.Item(pcCase.ESata)));
                            }

                            if (pcCase.Firewire != null && pcCase.Firewire != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Firewire", _dimension.Item(pcCase.Firewire)));
                            }

                            if (pcCase.Sound != null && pcCase.Sound != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Аудиовыходы", _dimension.Item(pcCase.Sound)));
                            }

                            if (pcCase.Mic != null && pcCase.Mic != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Разъемы для микрофона", _dimension.Item(pcCase.Mic)));
                            }

                            if (pcCase.Window)
                            {
                                model.Specs.Add(new Tuple<string, string>("Прозрачное окно", "есть"));
                            }
                            
                            if (pcCase.Material != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Материал корпуса", pcCase.Material));
                            }
                            
                            if (pcCase.PsuInstalled)
                            {
                                model.Specs.Add(new Tuple<string, string>("Блок питания в комплекте", "есть"));
                            }
                            
                            if (pcCase.PsuPower != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Блок питания в комплекте", _dimension.EnerW(pcCase.PsuPower)));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    case 4:
                        var caseFan = _dbContext.ViewCaseFans.FirstOrDefault(v => v.Id == product.Id);
                        if (caseFan != null)
                        {
                            if (caseFan.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", caseFan.Manufacturer));
                            }

                            if (caseFan.FanSize != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Размер", caseFan.FanSize));
                            }
                            
                            if (caseFan.Noise != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Шум", _dimension.NoiseDb(caseFan.Noise)));
                            }
                            
                            if (caseFan.Speed != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Скорость вращения", _dimension.SpeedSpinsPreMinute(caseFan.Speed)));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    case 5:
                        var cpuCooler = _dbContext.ViewCpuCoolers.FirstOrDefault(v => v.Id == product.Id);
                        if (cpuCooler != null)
                        {
                            if (cpuCooler.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", cpuCooler.Manufacturer));
                            }

                            if (cpuCooler.CoolerType != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Тип кулера", cpuCooler.CoolerType));
                            }

                            if (cpuCooler.Socket != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Сокет", cpuCooler.Socket));
                            }
                            
                            if (cpuCooler.Tdp != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Максимальное тепловыделение процессора", _dimension.EnerW(cpuCooler.Tdp)));
                            }
                            
                            if (cpuCooler.Height != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Высота кулера", _dimension.LenMm(cpuCooler.Height)));
                            }
                            
                            if (cpuCooler.Speed != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Скорость вращения", _dimension.SpeedSpinsPreMinute(cpuCooler.Speed)));
                            }
                            
                            if (cpuCooler.Noise != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Шум", _dimension.NoiseDb(cpuCooler.Noise)));
                            }
                            
                            if (cpuCooler.Weight != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Вес", _dimension.WeightGr(cpuCooler.Weight)));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    case 6:
                        var hdd = _dbContext.ViewHdds.FirstOrDefault(v => v.Id == product.Id);
                        if (hdd != null)
                        {
                            if (hdd.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", hdd.Manufacturer));
                            }

                            if (hdd.FormFactor != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Форм-фактор", hdd.FormFactor));
                            }

                            if (hdd.Interface != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Интерфейс", hdd.Interface));
                            }

                            if (hdd.SpindleSpeed != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Скорость вращения", _dimension.SpeedSpinsPreMinute(hdd.SpindleSpeed)));
                            }
                            
                            if (hdd.Capacity != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Объем", _dimension.MemoryTb(hdd.Capacity)));
                            }
                            
                            if (hdd.Cache != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Буфер памяти", _dimension.MemoryMb(hdd.Cache)));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    case 7:
                        var motherboard = _dbContext.ViewMotherboards.FirstOrDefault(v => v.Id == product.Id);
                        if (motherboard != null)
                        {
                            if (motherboard.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", motherboard.Manufacturer));
                            }
                            
                            if (motherboard.Socket != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Сокет", motherboard.Socket));
                            }
                            
                            if (motherboard.FormFactor != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Форм-фактор", motherboard.FormFactor));
                            }
                            
                            if (motherboard.Chipset != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Чипсет", motherboard.Chipset));
                            }
                            
                            if (motherboard.RamTechnology != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Тип оперативной памяти", motherboard.RamTechnology));
                            }
                            
                            if (motherboard.RamTypes != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Вид оперативной памяти", motherboard.RamTypes));
                            }
                            
                            if (motherboard.RamSlots != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество слотов под оперативную память", _dimension.Item(motherboard.RamSlots)));
                            }
                            
                            if (motherboard.RamMaxTotalSize != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Максимальный объем оперативной памяти", _dimension.MemoryGb(motherboard.RamMaxTotalSize)));
                            }
                            
                            if (motherboard.Pciex16version != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Версия PCIe", motherboard.Pciex16version));
                            }
                            
                            if (motherboard.Pciex16 != null && motherboard.Pciex16 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("PCIe x16", _dimension.Item(motherboard.Pciex16)));
                            }
                            
                            if (motherboard.Pciex4 != null && motherboard.Pciex4 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("PCIe x4", _dimension.Item(motherboard.Pciex4)));
                            }
                            
                            if (motherboard.Pciex1 != null && motherboard.Pciex1 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("PCIe x1", _dimension.Item(motherboard.Pciex1)));
                            }
                            
                            if (motherboard.Pci != null && motherboard.Pci != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("PCI", _dimension.Item(motherboard.Pci)));
                            }
                            
                            if (motherboard.M2 != null && motherboard.M2 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("M2", _dimension.Item(motherboard.M2)));
                            }

                            if (motherboard.Sata3raid)
                            {
                                model.Specs.Add(new Tuple<string, string>("Поддержка SATA3 RAID", "есть"));
                            }
                            
                            if (motherboard.Sata3 != null && motherboard.Sata3 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("SATA3", _dimension.Item(motherboard.Sata3)));
                            }
                            
                            if (motherboard.Sata2raid)
                            {
                                model.Specs.Add(new Tuple<string, string>("Поддержка SATA2 RAID", "есть"));
                            }
                            
                            if (motherboard.Sata2 != null && motherboard.Sata2 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("SATA2", _dimension.Item(motherboard.Sata2)));
                            }
                            
                            if (motherboard.Thunderbolt != null && motherboard.Thunderbolt != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("Thunderbolt", _dimension.Item(motherboard.Thunderbolt)));
                            }
                            
                            if (motherboard.UsbTypeC != null && motherboard.UsbTypeC != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("USB Type-C", _dimension.Item(motherboard.UsbTypeC)));
                            }
                            
                            if (motherboard.Usb31 != null && motherboard.Usb31 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("USB 3.1", _dimension.Item(motherboard.Usb31)));
                            }
                            
                            if (motherboard.Usb30 != null && motherboard.Usb30 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("USB 3.0", _dimension.Item(motherboard.Usb30)));
                            }
                            
                            if (motherboard.Usb20 != null && motherboard.Usb20 != 0)
                            {
                                model.Specs.Add(new Tuple<string, string>("USB 2.0", _dimension.Item(motherboard.Usb20)));
                            }
                            
                            if (motherboard.WiFi)
                            {
                                model.Specs.Add(new Tuple<string, string>("WiFi", "есть"));
                            }
                            
                            if (motherboard.Sli)
                            {
                                model.Specs.Add(new Tuple<string, string>("Поддержка SLI", "есть"));
                            }
                            
                            if (motherboard.Crossfire)
                            {
                                model.Specs.Add(new Tuple<string, string>("Поддержка Crossfire", "есть"));
                            }
                            
                            if (motherboard.CpuPinPowering != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Питание процессора", motherboard.CpuPinPowering));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    case 8:
                        var psu = _dbContext.ViewPsus.FirstOrDefault(v => v.Id == product.Id);
                        if (psu != null)
                        {
                            if (psu.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", psu.Manufacturer));
                            }
                            
                            if (psu.Power != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Мощность", _dimension.EnerW(psu.Power)));
                            }

                            if (psu.FormFactor != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Форм-фактор", psu.FormFactor));
                            }
                            
                            if (psu.Pfc)
                            {
                                model.Specs.Add(new Tuple<string, string>("Активный PFC", "есть"));
                            }

                            if (psu.Plus != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Сертификат PLUS", psu.Plus));
                            }

                            if (psu.Sata != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("SATA", _dimension.Item(psu.Sata)));
                            }
                            
                            if (psu.Molex != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Molex", _dimension.Item(psu.Molex)));
                            }
                            
                            if (psu.Pciex24 != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("PCIe 24-pin", _dimension.Item(psu.Pciex24)));
                            }
                            
                            if (psu.Pciex8 != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("PCIe 8-pin", _dimension.Item(psu.Pciex8)));
                            }
                            
                            if (psu.Pciex6 != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("PCIe 6-pin", _dimension.Item(psu.Pciex6)));
                            }
                            
                            if (psu.Pciex4 != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("PCIe 4-pin", _dimension.Item(psu.Pciex4)));
                            }
                            
                            if (psu.Pciex2 != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("PCIe 2-pin", _dimension.Item(psu.Pciex2)));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    case 9:
                        var ram = _dbContext.ViewRams.FirstOrDefault(v => v.Id == product.Id);
                        if (ram != null)
                        {
                            if (ram.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", ram.Manufacturer));
                            }

                            if (ram.RamTechnology != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Тип памяти", ram.RamTechnology));
                            }

                            if (ram.RamType != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Вид памяти", ram.RamType));
                            }

                            if (ram.CapacityPerModule != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Объем памяти", _dimension.MemoryGb(ram.CapacityPerModule)));
                            }
                            
                            if (ram.Modules != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество модулей", _dimension.Item(ram.Modules)));
                            }
                            
                            if (ram.Frequency != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Количество потоков", _dimension.FreqMHz(ram.Frequency)));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    case 10:
                        var ssd = _dbContext.ViewSsds.FirstOrDefault(v => v.Id == product.Id);
                        if (ssd != null)
                        {
                            if (ssd.Manufacturer != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Производитель", ssd.Manufacturer));
                            }
                            
                            if (ssd.Capacity != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Объем", _dimension.MemoryTb(ssd.Capacity)));
                            }

                            if (ssd.SsdFormFactor != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Форм-фактор", ssd.SsdFormFactor));
                            }

                            if (ssd.SsdInterface != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Интерфейс", ssd.SsdInterface));
                            }

                            if (ssd.SsdTechnology != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Технология памяти", ssd.SsdTechnology));
                            }
                            
                            if (ssd.Nvme)
                            {
                                model.Specs.Add(new Tuple<string, string>("Поддержка NVMe", "есть"));
                            }

                            if (ssd.ReadSpeed != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Скорость чтения", _dimension.MemoryMbPerSecond(ssd.ReadSpeed)));
                            }
                            
                            if (ssd.WriteSpeed != null)
                            {
                                model.Specs.Add(new Tuple<string, string>("Скорость записи", _dimension.MemoryMbPerSecond(ssd.WriteSpeed)));
                            }
                            
                            if (ssd.HardwareEncryption)
                            {
                                model.Specs.Add(new Tuple<string, string>("Аппаратное шифрование", "есть"));
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        break;
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            
            return View(model);
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

                    if (model.Cpu.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.Cpu.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Cpu.Product.Image = fileName;
                }
                else if (model.Cpu.Product.Image == null)
                {
                    model.Cpu.Product.Image = "cpu.svg";
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
                    
                    if (model.Gpu.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.Gpu.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Gpu.Product.Image = fileName;
                }
                else if (model.Gpu.Product.Image == null)
                {
                    model.Gpu.Product.Image = "gpu.svg";
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
                    
                    if (model.Case.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.Case.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Case.Product.Image = fileName;
                }
                else if (model.Case.Product.Image == null)
                {
                    model.Case.Product.Image = "pc-case.svg";
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
                    
                    if (model.CaseFan.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.CaseFan.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.CaseFan.Product.Image = fileName;
                }
                else if (model.CaseFan.Product.Image == null)
                {
                    model.CaseFan.Product.Image = "cooling-fan.svg";
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
                    
                    if (model.CpuCooler.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.CpuCooler.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.CpuCooler.Product.Image = fileName;
                }
                else if (model.CpuCooler.Product.Image == null)
                {
                    model.CpuCooler.Product.Image = "cooling-fan.svg";
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
                    
                    if (model.Hdd.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.Hdd.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Hdd.Product.Image = fileName;
                }
                else if (model.Hdd.Product.Image == null)
                {
                    model.Hdd.Product.Image = "hdd.svg";
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
                    
                    if (model.Motherboard.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.Motherboard.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Motherboard.Product.Image = fileName;
                }
                else if (model.Motherboard.Product.Image == null)
                {
                    model.Motherboard.Product.Image = "motherboard.svg";
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
                    
                    if (model.Psu.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.Psu.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Psu.Product.Image = fileName;
                }
                else if (model.Psu.Product.Image == null)
                {
                    model.Psu.Product.Image = "power-supply.svg";
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
                    
                    if (model.Ram.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.Ram.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Ram.Product.Image = fileName;
                }
                else if (model.Ram.Product.Image == null)
                {
                    model.Ram.Product.Image = "ram.svg";
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
                    
                    if (model.Ssd.Product.Image != null)
                    {
                        System.IO.File.Delete(Path.Combine(uploadsFolder, model.Ssd.Product.Image));
                    }
                    
                    _processor.ProcessPicture(model.Image, filePath, _resizer, _compresser, 300);

                    model.Ssd.Product.Image = fileName;
                }
                else if (model.Ssd.Product.Image == null)
                {
                    model.Ssd.Product.Image = "ssd.svg";
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