using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace configurator_shop.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ILogger<AuthorizationController> _logger;
        
        public AuthorizationController(ILogger<AuthorizationController> logger)
        {
            _logger = logger;
        }

        // GET
        public IActionResult Confirmation()
        {
            return View();
        }
        
        public IActionResult Recovery()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult Register()
        {
            return View();
        }
        
        public IActionResult Profile()
        {
            return View();
        }
    }
}