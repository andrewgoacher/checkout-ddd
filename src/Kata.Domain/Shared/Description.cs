﻿namespace Kata.Domain.Shared
{
    public record Description
    {
        private string _value;
        public Description(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidDescriptionException();
            }

            _value = value;
        }

        public static implicit operator string(Description desc) => desc._value;
    }
}