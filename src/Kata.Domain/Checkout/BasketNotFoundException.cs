using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class BasketNotFoundException : DomainException
    {
        public BasketNotFoundException(BasketId basketId) : base($"The basket with id ({basketId}) does not exist")
        {
        }
    }
}
