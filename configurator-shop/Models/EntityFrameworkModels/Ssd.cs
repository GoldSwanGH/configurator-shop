using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class Ssd
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manudacturer { get; set; }
        public int? FormFactor { get; set; }
        public int? Interface { get; set; }
        public int? Capacity { get; set; }
        public int? Technology { get; set; }
        public int? Nvme { get; set; }
        public int? ReadSpeed { get; set; }
        public int? WriteSpeed { get; set; }
        public int? Encryption { get; set; }
    }
}
