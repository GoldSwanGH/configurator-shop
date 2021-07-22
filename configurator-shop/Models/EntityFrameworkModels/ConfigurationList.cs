using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class ConfigurationList
    {
        public int Id { get; set; }
        public int ConfId { get; set; }
        public int ProductId { get; set; }

        public virtual Configuration Conf { get; set; }
        public virtual Product Product { get; set; }
    }
}
