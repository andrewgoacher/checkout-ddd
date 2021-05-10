using System;
namespace Kata.Domain.Core
{
    public abstract class DomainException : Exception
    {
        public DomainException(string description)
        {
            Description = description;
        }

        public string Description { get; }
    }
}
