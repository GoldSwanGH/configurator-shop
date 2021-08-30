using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using configurator_shop.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using configurator_shop.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace configurator_shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISmtpEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ISmtpEmailSender emailSender)
        {
            _logger = logger;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriteUsBack(string controller, string action, string text)
        {
            var to = new MailboxAddress("admin", _configuration["SmtpConfiguration:SmtpUser"]);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<p>" + text + "</p>";
            bodyBuilder.TextBody = text;
            
            var sendEmail = _emailSender.TryToSendMail(to, "Сообщение от пользователя", bodyBuilder.ToMessageBody());
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}