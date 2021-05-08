using System;

namespace Kata.Domain.Checkout
{
    public class InvalidBasketException : Exception
    {
        public string Error { get; }

        public InvalidBasketException(string error)
        {
            Error = error;
        }
    }
}