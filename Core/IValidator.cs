namespace Core
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T value, ValidationResult validationResult);
    }
}