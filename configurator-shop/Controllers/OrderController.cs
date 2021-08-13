using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace configurator_shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        
        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        public IActionResult YourOrder()
        {
            return View();
        }
        
        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult OrderInfo()
        {
            return View();
        }
    }
}