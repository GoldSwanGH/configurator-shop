using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class User
    {
        public User()
        {
            Configurations = new HashSet<Configuration>();
            OrderInfos = new HashSet<OrderInfo>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string Tel { get; set; }
        public string Token { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool CustomImage { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual ICollection<Configuration> Configurations { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
