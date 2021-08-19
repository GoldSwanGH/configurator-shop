using System.ComponentModel;
using configurator_shop.Models.EntityFrameworkModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace configurator_shop.Models.ViewModels
{
    public class ProfileViewModel : UserViewModel
    {
        [DisplayName("Сменить изображение профиля")]
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
            
            if (model.CustomImage)
            {
                model.ImagePath = @"~/images/users/" + model.Id + ".jpg";
            }
            else
            {
                model.ImagePath = @"~/images/users/user.svg";
            }

            return model;
        }
    }
}