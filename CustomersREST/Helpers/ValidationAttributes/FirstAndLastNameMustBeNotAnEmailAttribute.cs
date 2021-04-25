namespace CustomersREST.Helpers.ValidationAttributes
{
    using CustomersREST.Models;
    using System.ComponentModel.DataAnnotations;

    public class FirstAndLastNameMustBeNotAnEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (CustomerForCreationDto)validationContext.ObjectInstance;
            if(customer.FirstName == customer.Email || customer.LastName == customer.Email)
            {
                return new ValidationResult(
                    ErrorMessage,
                    new[] { nameof(CustomerForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
