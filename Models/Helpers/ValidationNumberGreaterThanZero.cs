using System.ComponentModel.DataAnnotations;

namespace LocateMyCarWeb.Models.Helpers
{
    public class ValidationNumberGreaterThanZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                double numericValue;

                if (double.TryParse(value.ToString(), out numericValue))
                {
                    if (numericValue > 0)
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
