using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class InvalidItemIdException : ValidationException
    {
        public InvalidItemIdException() : base("The id is invalid")
        {

        }
    }
}