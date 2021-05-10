using Kata.Domain.Core;

namespace Kata.Domain.Shared
{
    public class InvalidQuantityException :  ValidationException
    {
        public InvalidQuantityException() : base("Quantity cannot be less than zero")
        {
        }
    }
}
