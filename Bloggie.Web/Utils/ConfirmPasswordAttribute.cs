using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Utils
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConfirmPasswordAttribute : ValidationAttribute
    {
        private readonly string _propertyNameMatch;
        public ConfirmPasswordAttribute(string propertyNameMatch)
        {
            _propertyNameMatch = propertyNameMatch;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_propertyNameMatch);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property with name {_propertyNameMatch} not found");
            }

            var propertValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || propertValue == null || !value.Equals(propertValue.ToString()))
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be match {_propertyNameMatch}");
            }

            return ValidationResult.Success;
        }
    }
}