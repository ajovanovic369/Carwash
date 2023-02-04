using System.ComponentModel.DataAnnotations;

namespace CarWash.Validations
{
    public class EmailAddressValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }


            var input = value.ToString();
            int check = input.Length;

            if (input.ToString()[check - 1].ToString() != "m" || input.ToString()[check - 2].ToString() != "o" || input.ToString()[check - 3].ToString() != "c" || input.ToString()[check - 4].ToString() != ".")
            {
                return new ValidationResult("EmailAdress must contain .com at the end");
            }


            return ValidationResult.Success;
        }
    }
}
