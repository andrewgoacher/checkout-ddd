using System;

namespace Kata.Domain.Checkout
{

    [System.Diagnostics.DebuggerDisplay("{_id}")]
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

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}