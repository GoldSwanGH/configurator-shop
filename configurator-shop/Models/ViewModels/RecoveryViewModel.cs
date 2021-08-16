using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace configurator_shop.Models.ViewModels
{
    public class RecoveryViewModel : UserViewModel
    {
        [Required(ErrorMessage = "Это обязательное поле.")]
        [EmailAddress(ErrorMessage = "Email адрес введен некорректно.")]
        [DisplayName("Почта")]
        public override string Email { get; set; }
    }
}