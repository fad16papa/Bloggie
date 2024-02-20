using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Bloggie.Web.Utils
{
    //This will check the password if it has all the required regex.
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PasswordValidationAttribute : ValidationAttribute
    {
        private readonly string _pattern;

        public PasswordValidationAttribute(string pattern)
        {
            _pattern = pattern;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string password = value.ToString();

                if (!Regex.IsMatch(password, _pattern))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
