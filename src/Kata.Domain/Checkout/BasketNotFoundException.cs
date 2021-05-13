using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class BasketNotFoundException : AggregateNotFoundException
    {
        public BasketNotFoundException(BasketId basketId) : base($"The basket with id ({basketId}) does not exist")
        {
        }
    }
}
