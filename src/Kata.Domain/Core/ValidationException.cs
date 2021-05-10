namespace Kata.Domain.Core
{
    public abstract class ValidationException : DomainException
    {
        public ValidationException(string field, string value) : base($"The field ({field}) with value ({value}) is invalid")
        {
        }

        public ValidationException(string description) : base(description) { }
    }
}
