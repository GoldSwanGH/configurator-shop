using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Attributes;

namespace configurator_shop.Models.ViewModels
{
    public class LoginViewModel : UserViewModel
    {
        [Required(ErrorMessage = "Это обязательное поле.")]
        [EmailAddress(ErrorMessage = "Email адрес введен неправильно.")]
        [DisplayName("Почта")]
        [CheckEmailAvailability(true)]
        public override string Email { get; set; }
        
        [Required(ErrorMessage = "Это обязательное поле.")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}