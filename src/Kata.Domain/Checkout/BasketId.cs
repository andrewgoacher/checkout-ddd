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
            Id = id;
        }
        
        public Guid Id { get; }

        public static implicit operator Guid(BasketId basketId) => basketId.Id;
    }
}