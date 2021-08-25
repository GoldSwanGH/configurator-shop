using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace configurator_shop.Models.ViewModels
{
    public class LoadImageViewModel : ImageViewModel
    {
        [DisplayName("Загрузить изображение товара")]
        [BindProperty]
        public IFormFile Image { get; set; }
    }
}