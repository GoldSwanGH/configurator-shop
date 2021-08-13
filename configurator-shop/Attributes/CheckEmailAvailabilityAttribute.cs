using System.ComponentModel.DataAnnotations;
using System.Linq;
using configurator_shop.Models.EntityFrameworkModels;
using configurator_shop.Models.ViewModels;

namespace configurator_shop.Attributes
{
    public class CheckEmailAvailabilityAttribute : ValidationAttribute
    {
        private ShopDbContext _dbcontext;

        private bool ExistenceExpected;

        public CheckEmailAvailabilityAttribute(bool existenceExpected)
        {
            this.ExistenceExpected = existenceExpected;
        }

        private string GetErrorMessage()
        {
            string errorMessage = "Пользователь с таким Email уже существует.";

            if (ExistenceExpected)
            {
                errorMessage = "Пользователя с таким Email не существует.";
            }

            return errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            _dbcontext = validationContext.GetService(typeof(ShopDbContext)) as ShopDbContext;
            
            var userViewModel = (UserViewModel)validationContext.ObjectInstance;
            var sameEmailUser = _dbcontext.Users.FirstOrDefault(u => u.Email == userViewModel.Email);
            
            if (!ExistenceExpected && sameEmailUser != null)
            {
                return new ValidationResult(GetErrorMessage());
            }
            if (ExistenceExpected && sameEmailUser == null)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}