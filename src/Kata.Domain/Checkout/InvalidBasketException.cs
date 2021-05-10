using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class InvalidBasketException : ValidationException
    {

        public InvalidBasketException(string description) : base(description)
        {
        }

        public InvalidBasketException(string field, string value) : base(field, value) { }
    }
}