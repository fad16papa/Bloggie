using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web.Utils
{
    //This will check if the email was already been used in UserManager
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string email = value.ToString();

                // Retrieve UserManager from the validation context
                var userManager = (UserManager<IdentityUser>)validationContext.GetService(typeof(UserManager<IdentityUser>));

                // Check if the email is already in use
                Task<IdentityUser> userTask = userManager.FindByEmailAsync(email);
                IdentityUser existingUser = userTask.Result;

                if (existingUser != null)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}