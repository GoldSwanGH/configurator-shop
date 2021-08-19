using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using configurator_shop.Filters;
using configurator_shop.Interfaces;
using configurator_shop.Models.EntityFrameworkModels;
using configurator_shop.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using BC = BCrypt.Net.BCrypt;

namespace configurator_shop.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly ShopDbContext _dbContext;
        private readonly ISmtpEmailSender _emailSender;
        private readonly ITokenizer _tokenizer;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICompresser _compresser;
        private readonly IResizer _resizer;
        
        public AuthorizationController(ILogger<AuthorizationController> logger, ShopDbContext dbContext, ISmtpEmailSender emailSender, ITokenizer tokenizer, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, ICompresser compresser, IResizer resizer)
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailSender = emailSender;
            _tokenizer = tokenizer;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _compresser = compresser;
            _resizer = resizer;
        }

        [HttpGet]
        [AnonymousOnlyFilter]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [AnonymousOnlyFilter]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = registerViewModel.ToUser();
                UserRole userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(r => r.RoleName == "User");

                if (userRole != null)
                {
                    user.Role = userRole;
                    user.RoleId = userRole.Id;
                }
                else
                {
                    return View(registerViewModel);
                }

                return RedirectToAction("SendConfirmationLetter", user);
            }   
            
            return View(registerViewModel); // Если валидация не прошла, возвращаемся на страницу регистрации.
        }
        
        [AnonymousOnlyFilter]
        public IActionResult SendConfirmationLetter(User user)
        {
            User sameUser = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            
            int responceId;
            
            if (sameUser == null)
            {
                string token = _tokenizer.GetRandomToken();
                string url = "https://" + HttpContext.Request.Host + "/Authorization/Confirmation/" + token;
                user.Token = token;

                var to = new MailboxAddress(user.FirstName, user.Email);
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<p>Пройдите по ссылке, чтобы подтвердить регистрацию:</p><br /><a href=\"" 
                                       + url + "\">" + url + "</a>";
                bodyBuilder.TextBody = "Пройдите по ссылке, чтобы подтвердить регистрацию:\n" + url;
            
                var sendEmail = _emailSender.TryToSendMail(to, "Подтверждение регистрации", bodyBuilder.ToMessageBody());

                if (sendEmail == EmailResult.SendSuccess)
                {
                    responceId = 1;
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();
                }
                else
                {
                    responceId = 2;
                    //text = "Ошибка при отправке письма, проверьте введенную Вами при регистрации почту " + 
                                       //"и попробуйте пройти регистрацию еще раз.";
                }
            }
            else
            {
                responceId = 3;
                //text = "Ошибка при отправке письма.";
            }
            
            return RedirectToAction("Confirmation", "Authorization", new { id = responceId});
        }
        
        [Route("Authorization/Confirmation/{responseId:int}")]
        public IActionResult Confirmation(int responseId)
        {   
            switch (responseId)
            {
                case 1:
                    ViewData["Text"] = "Ваш аккаунт был создан. Чтобы подтвердить регистрацию и активировать аккаунт, " +
                                        "пройдите по ссылке из письма, которое мы отправили Вам на почту.";
                    break;
                case 2:
                    ViewData["Text"] = "Ошибка при отправке письма. Проверьте введенную Вами при регистрации почту " + 
                                        "и попробуйте пройти регистрацию еще раз.";
                    break;
                default:
                    ViewData["Text"] = "Ошибка при отправке письма.";
                    break;
            }
        
            return View();
        }
        
        [Route("Authorization/Confirmation/{token}")]
        public async Task<IActionResult> Confirmation(string token)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Token == token);
            
            if (user != null)
            {
                user.Token = null;
                user.EmailConfirmed = true;
                await _dbContext.SaveChangesAsync();
                
                return RedirectToAction("UserCreated");
            }
            
            ViewData["Text"] = "Ошибка. Скорее всего, Ваша ссылка нерабочая. Попробуйте зарегистрироваться снова.";
            
            return View();
        }
        
        public IActionResult UserCreated()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Recovery()
        {
            ViewData["Post"] = false;
            return View();
        }

        [HttpPost]
        public IActionResult Recovery(UserViewModel recoveryViewModel)
        {
            ViewData["Post"] = true;

            string userEmail;

            if (User.Identity is {IsAuthenticated: true})
            {
                userEmail = User.Identity.Name;
            }
            else
            {
                userEmail = recoveryViewModel.Email;
            }
            
            User sameUser = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
            if (sameUser != null)
            {
                string token = _tokenizer.GetRandomToken();
                string url = "https://" + HttpContext.Request.Host + "/Authorization/PasswordChange/" + token;

                var to = new MailboxAddress(sameUser.FirstName, sameUser.Email);
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<p>Пройдите по ссылке, чтобы изменить пароль:</p><br /><a href=\"" 
                                       + url + "\">" + url + "</a>";
                bodyBuilder.TextBody = "Пройдите по ссылке, чтобы изменить пароль:\n" + url;
            
                var sendEmail = _emailSender.TryToSendMail(to, "Изменение пароля", bodyBuilder.ToMessageBody());

                if (sendEmail == EmailResult.SendSuccess)
                {
                    sameUser.Token = token;
                    _dbContext.SaveChanges();
                }
            }
            return View();
        }
        
        [HttpGet]
        [Route("Authorization/PasswordChange/{token}")]
        public IActionResult PasswordChange(string token)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Token == token);
            
            if (user == null)
            {
                ViewData["Post"] = true;
                ViewData["Text"] = "Ошибка. Неверная ссылка.";
                return View();
            }
            else
            {
                ViewData["Post"] = false;
            }

            var passwordChangeViewModel = new PasswordChangeViewModel();
            passwordChangeViewModel.Email = user.Email;
            
            return View(passwordChangeViewModel);
        }
        
        [HttpPost]
        public IActionResult PasswordChange(PasswordChangeViewModel passwordChangeViewModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == passwordChangeViewModel.Email);
            ViewData["Post"] = true;
            if (user != null)
            {
                user.Password = BC.HashPassword(passwordChangeViewModel.Password);
                user.Token = null;
                _dbContext.SaveChanges();

                if (User.Identity is {IsAuthenticated: true})
                {
                    return RedirectToAction("Logout");
                }

                ViewData["Text"] = "Пароль успешно сменен. Пожалуйста, войдите в учетную запись с новым паролем.";
            }
            else
            {
                ViewData["Text"] = "Ошибка. Пользователь не найден";
            }
            return View();
        }
        
        [HttpGet]
        [AnonymousOnlyFilter]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AnonymousOnlyFilter]
        public async Task<IActionResult> Login(LoginViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _dbContext.Users
                    .Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == userViewModel.Email);
                if (user != null && BC.Verify(userViewModel.Password, user.Password))
                {
                    if (!user.EmailConfirmed)
                    {
                        ModelState.AddModelError("", "Ваша учетная запись еще не была активирована.");
                        return View();
                    }
                    await Authenticate(user);
 
                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        public IActionResult Profile()
        {
            User user;
            
            user = _dbContext.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            var model = ProfileViewModel.ToProfileViewModel(user);

            var check = User;
            
            return View(model);
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    if (model.FirstName != user.FirstName)
                    {
                        user.FirstName = model.FirstName;
                    }
                    if (model.LastName != user.LastName)
                    {
                        user.LastName = model.LastName;
                    }
                    if (model.Tel != user.Tel)
                    {
                        user.Tel = model.Tel;
                    }

                    if (model.Image != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "users");
                        string filePath = Path.Combine(uploadsFolder, user.Id + ".jpg");

                        if (model.Image == null || model.Image.Length == 0 /*|| model.Image.ContentType != "jpeg" Проверять тип файла?*/ )
                        {
                            ModelState.AddModelError("", "Ошибка изображения");
                            return View(model);
                        }
                        
                        if (user.CustomImage)
                        {
                            System.IO.File.Delete(filePath);
                        }
                        else
                        {
                            user.CustomImage = true;
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            model.Image.CopyTo(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                memoryStream.SetLength(0);
                                Image readyImage = _resizer.Resize(img, 200, 200);
                                readyImage = _compresser.Compress(readyImage, 50L, memoryStream);
                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    readyImage.Save(fileStream, ImageFormat.Jpeg);
                                }
                            }
                        }
                    }

                    _dbContext.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь не найден");
                    return View(model);
                }
            }
            
            User newUser = _dbContext.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            model = ProfileViewModel.ToProfileViewModel(newUser);
            
            return View(model);
        }
        
        private async Task Authenticate(User user)
        {
            // создаем claims
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RoleName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}