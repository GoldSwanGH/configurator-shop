using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class OrderInfoViewModel
    {
        public OrderInfo Order { get; set; }
        public List<OrderCart> Cart { get; set; }
        public UserViewModel UserModel { get; set; }
        
        public bool NewOrder { get; set; }
    }
}