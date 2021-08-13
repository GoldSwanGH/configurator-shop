using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace configurator_shop.Controllers
{
    public class ConfiguratorController : Controller
    {
        private readonly ILogger<ConfiguratorController> _logger;
        
        public ConfiguratorController(ILogger<ConfiguratorController> logger)
        {
            _logger = logger;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Configuration()
        {
            return View();
        }
        
        public IActionResult ChangeConfiguration()
        {
            return View();
        }
        
        public IActionResult Configurations()
        {
            return View();
        }
    }
}