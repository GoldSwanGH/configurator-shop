using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Configuration
    {
        public Configuration()
        {
            ConfigurationLists = new HashSet<ConfigurationList>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Valid { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ConfigurationList> ConfigurationLists { get; set; }
    }
}
