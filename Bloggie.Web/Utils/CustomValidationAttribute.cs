using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Utils
{
    public class CustomValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // Your custom validation logic
            // Return true if the validation passes, otherwise false

            if (value != null && (int)value == 42)
            {
                return false; // Validation fails
            }

            return true; // Validation passes
        }

        public override string FormatErrorMessage(string name)
        {
            return "Custom error message";
        }
    }
}