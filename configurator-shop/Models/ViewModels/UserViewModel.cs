using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class UserViewModel
    {
        public int? Id { get; set; }
        
        [DataType(DataType.Text)]
        [DisplayName("Имя")]
        public virtual string FirstName { get; set; }
        
        [DataType(DataType.Text)]
        [DisplayName("Фамилия")]
        public virtual string LastName { get; set; }
        
        [EmailAddress(ErrorMessage = "Email адрес введен неправильно.")]
        [DisplayName("Почта")]
        public virtual string Email { get; set; }

        [Phone(ErrorMessage = "Номер телефона введен неправильно.")]
        [DisplayName("Телефон")]
        public virtual string Tel { get; set; }
        
        public bool CustomImage { get; set; }
        
        public string ImagePath { get; set; }
        
        /*
        public UserViewModel()
        {
            
        }
        
        public UserViewModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Tel = user.Tel;
            CustomImage = user.CustomImage;

            if (CustomImage)
            {
                ImagePath = @"~/images/users/" + Id + ".jpg";
            }
            else
            {
                ImagePath = @"~/images/users/user.svg";
            }
        }
        */
        
        public static UserViewModel ToUserViewModel(User user)
        {
            var model = new UserViewModel
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