using System.Collections.Generic;
using configurator_shop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace configurator_shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        
        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }

        public IActionResult Categories()
        {
            return View();
        }
        
        public IActionResult Products()
        {
            return View();
        }
        
        public IActionResult Product()
        {
            return View();
        }

        public IActionResult AddProductChooseCategory()
        {
            var model = new CategoriesViewModel();

            model.Categories = new List<string>()
            {
                "CPU",
                "GPU",
                "Motherboard",
                "Ram",
                "Psu",
                "Case",
                "HDD",
                "SDD",
                "Cooler",
                "Fan"
            };
            
            return View(model);
        }

        public IActionResult AddProduct(string category)
        {
            switch (category)
            {
                case "CPU":
                    
                    break;
                case "GPU":
                    
                    break;
                case "Motherboard":
                    
                    break;
                case "Ram":
                    
                    break;
                case "Psu":
                    
                    break;
                case "Case":
                    
                    break;
                case "HDD":
                    
                    break;
                case "SDD":
                    
                    break;
                case "Cooler":
                    
                    break;
                case "Fan":
                    
                    break;
                default:
                    
                    break;
            }

            return View();
        }
    }
}