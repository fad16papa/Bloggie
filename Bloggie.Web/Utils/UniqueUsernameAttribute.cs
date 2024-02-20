using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web.Utils
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string username = value.ToString();

                // Retrieve UserManager from the validation context
                var userManager = (UserManager<IdentityUser>)validationContext.GetService(typeof(UserManager<IdentityUser>));

                // Check if the username is already in use
                Task<IdentityUser> userTask = userManager.FindByNameAsync(username);
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