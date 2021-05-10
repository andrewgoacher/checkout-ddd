using Kata.Domain.Core;

namespace Kata.Domain.Shared
{
    public class TooManyDecimalPlacesException : ValidationException
    {
        public TooManyDecimalPlacesException() : base("Maximum of 2 decimal places allowed")
        {

        }
    }
}