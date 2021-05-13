namespace Kata.Domain.Core
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string description) : base(description)
        {
        }
    }
}
