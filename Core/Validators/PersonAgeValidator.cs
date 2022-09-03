using Core.Entities;

namespace Core.Validators
{
    public class PersonAgeValidator : IValidator<Person>
    {
        public ValidationResult Validate(Person value, ValidationResult validationResult)
        {
            if (value.Age < 0)
            {
                validationResult.AddErrorMessage("Age cannot be a Negative number");
            }

            if (value.Age > 120)
            {
                validationResult.AddErrorMessage("Age cannot be higher than 120");
            }

            return validationResult;
        }
    }
}