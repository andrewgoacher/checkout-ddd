using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class BasketExistsException : DomainException
    {
        public BasketExistsException(BasketId basketId) : base($"The basket with id ({basketId}) already exists")
        {
        }
    }
}
