using Kata.Domain.Core;
using Kata.Domain.Shared;

namespace Kata.Domain.Checkout
{
    public class InvalidItemQuantityException  : ValidationException
    {
        public InvalidItemQuantityException(Quantity qty) : base($"The quantity {qty} is invalid for items")
        {
        }
    }
}
