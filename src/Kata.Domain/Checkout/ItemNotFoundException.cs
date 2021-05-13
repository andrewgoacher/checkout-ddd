using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class ItemNotFoundException : EntityNotFoundException
    {
        public ItemNotFoundException(string description) : base(description)
        {
        }
    }
}
