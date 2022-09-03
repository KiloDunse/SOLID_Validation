namespace Core
{
    public class ValidationResult
    {
        public List<string> ErrorMessages { get; private set; } = new List<string>();

        public bool IsValid => !ErrorMessages.Any();

        public void AddErrorMessage(string message)
        {
            ErrorMessages.Add(message);
        }

        public void Join(List<string> list)
        {
            ErrorMessages.AddRange(list);
        }
    }
}