using Core.Entities;

namespace Core.Validators
{
    public class PersonFirstNameValidator : IValidator<Person>
    {
        public ValidationResult Validate(Person value, ValidationResult validationResult)
        {
            if (string.IsNullOrWhiteSpace(value.FirstName))
            {
                validationResult.AddErrorMessage("FirstName is NULL or Empty");
            }

            if (value.FirstName.Length < 6)
            {
                validationResult.AddErrorMessage("FirstName must have more than 5 chars");
            }

            if (value.FirstName.Equals("Test", StringComparison.OrdinalIgnoreCase))
            {
                validationResult.AddErrorMessage("FirstName may not be 'Test'");
            }

            return validationResult;
        }
    }
}