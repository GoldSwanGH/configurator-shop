using System.ComponentModel;
using configurator_shop.Models.EntityFrameworkModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace configurator_shop.Models.ViewModels
{
    public class ProfileViewModel : UserViewModel
    {
        [DisplayName("Изображение профиля")]
        [BindProperty]
        public IFormFile Image { get; set; }
        
        public static ProfileViewModel ToProfileViewModel(User user)
        {
            var model = new ProfileViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Tel = user.Tel,
                CustomImage = user.CustomImage
            };
            return model;
        }
    }
}