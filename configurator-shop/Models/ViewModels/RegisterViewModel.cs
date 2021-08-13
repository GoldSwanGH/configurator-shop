using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Attributes;
using configurator_shop.Models.EntityFrameworkModels;
using BC = BCrypt.Net.BCrypt;

namespace configurator_shop.Models.ViewModels
{
    public class RegisterViewModel : UserViewModel
    {
        [Required(ErrorMessage = "Это обязательное поле.")]
        [EmailAddress(ErrorMessage = "Email адрес введен неправильно.")]
        [DisplayName("Почта")]
        [CheckEmailAvailability(false)]
        public override string Email { get; set; }
        
        [Required(ErrorMessage = "Это обязательное поле.")]
        [DataType(DataType.Text)]
        [DisplayName("Имя")]
        public override string FirstName { get; set; }
        
        [Required(ErrorMessage = "Это обязательное поле.")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 50 символов")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Это обязательное поле.")]
        [DataType(DataType.Password)]
        [DisplayName("Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public User ToUser()
        {
            var user = new User
            {
                Email = this.Email,
                Password = BC.HashPassword(this.Password),
                Tel = this.Tel,
                FirstName = this.FirstName,
                LastName = this.LastName
            };
            return user;
        }
    }
}