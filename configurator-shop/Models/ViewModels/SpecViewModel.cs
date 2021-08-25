using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using configurator_shop.Models.EntityFrameworkModels;

namespace configurator_shop.Models.ViewModels
{
    public class SpecViewModel
    {
        public bool SpecType { get; set; }
        public string SpecName { get; set; }
        public int Number { get; set; }
        public string Value { get; set; }
    }
}