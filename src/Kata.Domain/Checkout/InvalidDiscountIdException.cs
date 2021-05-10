using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class InvalidDiscountIdException : ValidationException
    {
        public InvalidDiscountIdException() : base ("The id is invalid")
        {
        }
    }
}
