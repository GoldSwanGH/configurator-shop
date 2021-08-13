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
    }
}