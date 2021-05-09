using System;

namespace Kata.Domain.Checkout
{
    public record BasketId
    {
        public BasketId(Guid id)
        {
            if (id == default)
            {
                throw new InvalidBasketIdException();
            }
            _id = id;
        }

        private Guid _id;

        public static implicit operator Guid(BasketId basketId) => basketId._id;
    }
}