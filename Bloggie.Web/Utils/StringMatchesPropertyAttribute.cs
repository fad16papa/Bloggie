using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Utils
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StringMatchesPropertyAttribute : ValidationAttribute
    {
        private readonly string _propertyNameToMatch;

        public StringMatchesPropertyAttribute(string propertyNameToMatch)
        {
            _propertyNameToMatch = propertyNameToMatch;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_propertyNameToMatch);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property with name {_propertyNameToMatch} not found");
            }

            var propertValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || propertValue == null || value.Equals(propertValue.ToString()))
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must not match {_propertyNameToMatch}");
            }

            return ValidationResult.Success;
        }
    }
}