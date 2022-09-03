using Core.Entities;

namespace Core.Validators
{
    public class EmployeeIdValidator : IValidator<Employee>
    {
        public ValidationResult Validate(Employee value, ValidationResult validationResult)
        {
            if (value.Id < 0)
            {
                validationResult.AddErrorMessage("Id cannot be a Negative number");
            }

            if (value.Id > 120)
            {
                validationResult.AddErrorMessage("Id cannot be higher than 120");
            }

            return validationResult;
        }
    }
}