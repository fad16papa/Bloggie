using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Bloggie.Web.Utils
{
    //This will check if the username is in small caps, numbers and some special characters
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UsernameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string username = value.ToString();

                if (!Regex.IsMatch(username, "^[a-z0-9_\\-]+$"))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}