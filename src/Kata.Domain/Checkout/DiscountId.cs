using System;
namespace Kata.Domain.Checkout
{

    [System.Diagnostics.DebuggerDisplay("{_id}")]
    public record DiscountId
    {
        private Guid _id;

        public DiscountId(Guid id)
        {
            if (id == default)
            {
                throw new InvalidDiscountIdException();
            }

            _id = id;
        }

        public static implicit operator Guid(DiscountId discount) => discount._id;

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}
