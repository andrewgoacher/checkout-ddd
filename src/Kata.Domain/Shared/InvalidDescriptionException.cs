using Kata.Domain.Core;

namespace Kata.Domain.Shared
{
    public class InvalidDescriptionException : ValidationException
    {
        public InvalidDescriptionException() : base("The description was a null or empty string")
        {
        }
    }
}
