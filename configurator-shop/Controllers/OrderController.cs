using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using configurator_shop.Interfaces;
using configurator_shop.Models;
using configurator_shop.Models.EntityFrameworkModels;
using configurator_shop.Models.ViewModels;
using configurator_shop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using Newtonsoft.Json;

namespace configurator_shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly ShopDbContext _dbContext;
        private readonly ISmtpEmailSender _emailSender;
        
        public OrderController(ILogger<OrderController> logger, ShopDbContext dbContext, ISmtpEmailSender emailSender)
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailSender = emailSender;
        }

        public IActionResult Cart()
        {
            var model = new CartViewModel();
            
            if (HttpContext.Request.Cookies.TryGetValue("Cart", out string value))
            {
                var cart = JsonConvert.DeserializeObject<CartItem[]>(value);

                if (cart.Length == 0)
                {
                    return View(model);
                }
                
                var idList = new List<int>();
                foreach (var item in cart)
                {
                    idList.Add(item.id);
                }

                var products = _dbContext.Products.Where(p => idList.Contains(p.Id)).ToList();

                foreach (var product in products)
                {
                    foreach (var item in cart)
                    {
                        if (product.Id == item.id)
                        {
                            model.Cart.Add(new Tuple<Product, int>(product, item.amount));
                            break;
                        }
                    }
                }
            }
            
            return View(model);
        }
        
        [Authorize]
        public IActionResult YourOrder()
        {
            var cartModel = new CartViewModel();
            
            if (HttpContext.Request.Cookies.TryGetValue("Cart", out string value))
            {
                var cart = JsonConvert.DeserializeObject<CartItem[]>(value);

                if (cart.Length == 0)
                {
                    return View("Cart", cartModel);
                }
                
                var idList = new List<int>();
                foreach (var item in cart)
                {
                    idList.Add(item.id);
                }

                var products = _dbContext.Products.Where(p => idList.Contains(p.Id)).ToList();

                foreach (var product in products)
                {
                    foreach (var item in cart)
                    {
                        if (product.Id == item.id)
                        {
                            cartModel.Cart.Add(new Tuple<Product, int>(product, item.amount));
                            break;
                        }
                    }
                }
            }

            var model = new OrderViewModel() {Cart = cartModel};

            if (User.Identity is {IsAuthenticated: true})
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                
                if (user != null)
                {
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.Tel = user.Tel;
                    model.Email = user.Email;
                }
            }
            
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult YourOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("NewOrder", model);
            }
            
            var cartModel = new CartViewModel();
            
            if (HttpContext.Request.Cookies.TryGetValue("Cart", out string value))
            {
                var cart = JsonConvert.DeserializeObject<CartItem[]>(value);

                if (cart.Length == 0)
                {
                    return View("Cart", cartModel);
                }
                
                var idList = new List<int>();
                foreach (var item in cart)
                {
                    idList.Add(item.id);
                }

                var products = _dbContext.Products.Where(p => idList.Contains(p.Id)).ToList();

                foreach (var product in products)
                {
                    foreach (var item in cart)
                    {
                        if (product.Id == item.id)
                        {
                            cartModel.Cart.Add(new Tuple<Product, int>(product, item.amount));
                            break;
                        }
                    }
                }
            }

            model.Cart = cartModel;

            return View(model);
        }

        [Authorize]
        public IActionResult NewOrder(OrderViewModel model)
        {
            int totalPrice = 0;
            var cartModel = new CartViewModel();
        
            if (HttpContext.Request.Cookies.TryGetValue("Cart", out string value))
            {
                var cart = JsonConvert.DeserializeObject<CartItem[]>(value);

                if (cart.Length == 0)
                {
                    return View("Cart", cartModel);
                }
            
                var idList = new List<int>();
                foreach (var item in cart)
                {
                    idList.Add(item.id);
                }

                var products = _dbContext.Products.Where(p => idList.Contains(p.Id)).ToList();

                foreach (var product in products)
                {
                    foreach (var item in cart)
                    {
                        if (product.Id == item.id)
                        {
                            cartModel.Cart.Add(new Tuple<Product, int>(product, item.amount));
                            totalPrice += product.Price * item.amount;
                            break;
                        }
                    }
                }
            }

            User user;
            
            if (User.Identity is {IsAuthenticated: true})
            {
                user = _dbContext.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            }
            else
            {
                var role = _dbContext.UserRoles.FirstOrDefault(r => r.RoleName == "Guest");
                if (role == null)
                {
                    return View("Cart", cartModel);
                }
                user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Tel = model.Tel,
                    Email = model.Email,
                    Password = "fic_user",
                    RoleId = role.Id,
                    Role = role,
                    EmailConfirmed = false,
                    CustomImage = false
                };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                user = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            }
            
            if (user == null)
            {
                return View("Cart", cartModel);
            }
            
            var order = new OrderInfo()
            {
                UserId = user.Id,
                User = user,
                OrderDate = DateTime.Now,
                Address = model.City + ", " + model.Street + ", "+ model.House + (model.Apartment != null ? ", кв." : "") + (model.Apartment ?? ""),
                Warranty = model.Warranty,
                Call = model.CourierCall,
                FastDelivery = model.FastDelivery,
                Test = model.Test,
                TotalPrice = totalPrice,
                Token = Guid.NewGuid().ToString()
            };

            _dbContext.OrderInfos.Add(order);
            _dbContext.SaveChanges();

            order = _dbContext.OrderInfos.FirstOrDefault(o =>
                o.Token == order.Token);
            
            if (order == null)
            {
                return View("Cart", cartModel);
            }

            order.Token = null;
            _dbContext.SaveChanges();
            
            foreach (var product in cartModel.Cart)
            {
                var orderCart = new OrderCart()
                {
                    OrderId = order.Id,
                    Order = order,
                    ProductId = product.Item1.Id,
                    Amount = product.Item2
                };
                
                _dbContext.OrderCarts.Add(orderCart);
            }
            
            _dbContext.SaveChanges();
            
            var to = new MailboxAddress(user.FirstName + " " + (user.LastName ?? ""), model.Email);
            var bodyBuilder = new BodyBuilder();
            
            bodyBuilder.HtmlBody = "<h2>Детали заказа</h2>" +
                                   "<p>Номер заказа:" + order.Id + "</p>" +
                                   "<p>Дата заказа:" + order.OrderDate + "</p>" +
                                   "<p>Сумма заказа:" + order.TotalPrice + "</p>" +
                                   "<h3>Заказчик:</h3>" +
                                   "<p>" + user.FirstName + " " + (user.LastName ?? "") + ", телефон " + user.Tel + "</p>" +
                                   "<h3>Адрес доставки:</h3>" +
                                   "<p>" + order.Address + "</p>" +
                                   "<h2>Список товаров:</h2>";
            
            bodyBuilder.TextBody = "Детали заказа \n\n" +
                                   "Номер заказа:" + order.Id + "\n" +
                                   "Дата заказа:" + order.OrderDate + "\n" +
                                   "Сумма заказа:" + order.TotalPrice + "\n\n" +
                                   "Заказчик:\n" +
                                   user.FirstName + user.LastName + ", телефон " + user.Tel + "\n" +
                                   "Адрес доставки:\n" +
                                   order.Address + "\n\n" +
                                   "Список товаров:\n\n";

            int i = 0;
            
            foreach (var product in cartModel.Cart)
            {
                i++;
                bodyBuilder.HtmlBody += "<h3>" + "Товар " + i + "</h3>" +
                                        "<p>" + (product.Item1.Name + " - " + product.Item1.Summary) + "</p>" +
                                        "<p>" + product.Item1.Price + "₽ - " + product.Item2 + " шт.</p>";
                bodyBuilder.TextBody += "Товар " + i + "\n" +
                                        (product.Item1.Name + " - " + product.Item1.Summary) + "\n" +
                                        product.Item1.Price + "₽ - " + product.Item2 + " шт.\n\n";
            }

            if (order.Call || order.Test || order.Warranty || order.FastDelivery)
            {
                bodyBuilder.HtmlBody += "<h3>Дополнительно:</h3>" +
                                        "<ul>";
                bodyBuilder.TextBody += "Дополнительно:\n";
                if (order.Call)
                {
                    bodyBuilder.HtmlBody += "<li>Звонок от курьера за час до доставки</li>";
                    bodyBuilder.TextBody += "Звонок от курьера за час до доставки\n";
                }
                if (order.Warranty)
                {
                    bodyBuilder.HtmlBody += "<li>Дополнительная гарантия 1 год</li>";
                    bodyBuilder.TextBody += "Дополнительная гарантия 1 год\n";
                }
                if (order.Test)
                {
                    bodyBuilder.HtmlBody += "<li>Тест всех товаров от наших специалистов</li>";
                    bodyBuilder.TextBody += "Тест всех товаров от наших специалистов\n";
                }
                if (order.FastDelivery)
                {
                    bodyBuilder.HtmlBody += "<li>Быстрая доставка</li>";
                    bodyBuilder.TextBody += "Быстрая доставка\n";
                }
                bodyBuilder.HtmlBody += "</ul>";
            }
            
            bodyBuilder.HtmlBody += "<h2>Спасибо за заказ!</h2>";
            bodyBuilder.TextBody += " \n";
            bodyBuilder.TextBody += "Спасибо за заказ!";
            
            var sendEmail = _emailSender.TryToSendMail(to, "Ваш заказ №" + order.Id, bodyBuilder.ToMessageBody());
            
            HttpContext.Response.Cookies.Delete("Cart");
            
            return RedirectToAction("OrderInfo", new{orderId = order.Id, newOrder = true});
        }
        
        public IActionResult ClearCart()
        {
            HttpContext.Response.Cookies.Delete("Cart");
            return RedirectToAction("Cart");
        }
        
        [Authorize]
        public IActionResult OrderInfo(int orderId, bool newOrder)
        {
            var order = _dbContext.OrderInfos.Include(o => o.User).FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = order.User;

            if (User.Identity != null && (user == null ||(user.Email != User.Identity.Name)))
            {
                return RedirectToAction("Index", "Home");
            }
            
            var cartItems = _dbContext.OrderCarts.Include(c => c.Product).Where(c => c.OrderId == order.Id).ToList();
            
            var model = new OrderInfoViewModel()
            {
                Order = order,
                Cart = cartItems,
                UserModel = UserViewModel.ToUserViewModel(user),
                NewOrder = newOrder
            };
            
            return View(model);
        }

        [Authorize]
        public IActionResult DownloadFile(int orderId)
        {
            var order = _dbContext.OrderInfos.Include(o => o.User)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var user = order.User;
            
            if (User.Identity != null && (user == null ||(user.Email != User.Identity.Name)))
            {
                return RedirectToAction("Index", "Home");
            }
            
            string text = "Детали заказа \n\n" +
                          "Номер заказа:" + order.Id + "\n" +
                          "Дата заказа:" + order.OrderDate + "\n" +
                          "Сумма заказа:" + order.TotalPrice + "\n\n" +
                          "Заказчик:\n" +
                          user.FirstName + " " + user.LastName + ", телефон " + user.Tel + "\n" +
                          "Адрес доставки:\n" +
                          order.Address + "\n\n" +
                          "Список товаров:\n\n";
            
            int i = 0;
            
            var cartItems = _dbContext.OrderCarts.Include(c => c.Product).Where(c => c.OrderId == order.Id).ToList();
            
            foreach (var product in cartItems)
            {
                i++;
                text += "Товар " + i + "\n" +
                        (product.Product.Name + " - " + product.Product.Summary) + "\n" +
                        product.Product.Price + "₽ - " + product.Amount + " шт.\n\n";
            }
            
            if (order.Call || order.Test || order.Warranty || order.FastDelivery)
            {
                text += "Дополнительно:\n";
                if (order.Call)
                {
                    text += "Звонок от курьера за час до доставки\n";
                }
                if (order.Warranty)
                {
                    text += "Дополнительная гарантия 1 год\n";
                }
                if (order.Test)
                {
                    text += "Тест всех товаров от наших специалистов\n";
                }
                if (order.FastDelivery)
                {
                    text += "Быстрая доставка\n";
                }
            }

            text += " \n";
            text += "Спасибо за заказ!";
            
            using (MemoryStream memoryStream = new MemoryStream())
            {
                TextWriter tw = new StreamWriter(memoryStream);

                tw.Write(text);
                tw.Flush();
                
                var length = memoryStream.Length;
                tw.Close();
                var toWrite = new byte[length];
                Array.Copy(memoryStream.GetBuffer(), 0, toWrite, 0, length);

                return File(toWrite, "text/plain", order.Id + ".txt");
            }
        }
    }
}