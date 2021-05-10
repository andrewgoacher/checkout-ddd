using Kata.Domain.Core;

namespace Kata.Domain.Shared
{
    public class InvalidPriceException : ValidationException
    {
        public InvalidPriceException() : base($"The price cannot be less than or equal to zero")
        {

        }
    }
}