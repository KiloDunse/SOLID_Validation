namespace Core
{
    public interface IValidationAggregator<TEntity> where TEntity : class
    {
        ValidationResult Validate(TEntity data);
    }

    public class ValidationAggregator<TEntity> : IValidationAggregator<TEntity> where TEntity : class
    {
        private readonly IEnumerable<IValidator<TEntity>> _validators;

        public ValidationAggregator(IEnumerable<IValidator<TEntity>> validators)
        {
            _validators = validators;
        }

        public ValidationResult Validate(TEntity data)
        {
            var result = new ValidationResult();

            foreach (var validator in _validators)
            {
                result = validator.Validate(data, result);
            }

            return result;
        }
    }
}