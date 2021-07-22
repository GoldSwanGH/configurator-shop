using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class OrderInfo
    {
        public OrderInfo()
        {
            OrderCarts = new HashSet<OrderCart>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderCart> OrderCarts { get; set; }
    }
}
