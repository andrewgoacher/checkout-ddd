using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class InvalidBasketIdException : ValidationException
    {
        public InvalidBasketIdException() : base("The id is invalid")
        {

        }
    }
}