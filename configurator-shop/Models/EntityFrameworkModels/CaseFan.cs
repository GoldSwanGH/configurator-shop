﻿using System;
using System.Collections.Generic;

#nullable disable

namespace configurator_shop.Models.EntityFrameworkModels
{
    public partial class CaseFan
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Manufacturer { get; set; }
        public int? FanSize { get; set; }
        public int? Height { get; set; }
        public int? Noise { get; set; }
        public int? Speed { get; set; }
    }
}
