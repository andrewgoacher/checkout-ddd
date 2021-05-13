namespace Kata.Domain.Core
{
    public class AggregateNotFoundException : DomainException
    {

        public AggregateNotFoundException(string description) : base(description)
        {
        }
    }
}
