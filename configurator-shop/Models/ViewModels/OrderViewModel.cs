using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Attributes;

namespace configurator_shop.Models.ViewModels
{
    public class OrderViewModel
    {
        public CartViewModel Cart { get; set; }
        
        [DataType(DataType.Text)]
        [DisplayName("Имя")]
        [Required(ErrorMessage = "Это поле обязательное.")]
        public string FirstName { get; set; }
        
        [DataType(DataType.Text)]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        
        [Phone(ErrorMessage = "Номер телефона введен неправильно.")]
        [DisplayName("Телефон")]
        [Required(ErrorMessage = "Это поле обязательное.")]
        public string Tel { get; set; }
        
        [EmailAddress(ErrorMessage = "Email адрес введен неправильно.")]
        [DisplayName("Почта")]
        [Required(ErrorMessage = "Это поле обязательное.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательное.")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательное.")]
        public string Street { get; set; }
        
        [Required(ErrorMessage = "Это поле обязательное.")]
        public string House { get; set; }
        
        public string Apartment { get; set; }
        
        public bool Warranty { get; set; }
        
        public bool CourierCall { get; set; }
        
        public bool Test { get; set; }
        
        public bool FastDelivery { get; set; }
    }
}