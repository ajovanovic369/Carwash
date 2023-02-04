using System.ComponentModel.DataAnnotations;

namespace CarWash.Validations
{
    public class UserNameValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var input = value.ToString();

            if (!input.All(char.IsLetter))
            {
                return new ValidationResult("UserName must contain only letters.");
            }

            return ValidationResult.Success;
        }
    }
}
