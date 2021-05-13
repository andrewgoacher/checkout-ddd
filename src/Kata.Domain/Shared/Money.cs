namespace Kata.Domain.Shared
{

    [System.Diagnostics.DebuggerDisplay("{_value}")]
    public record Money
    {
        public Money(decimal value)
        {
            if (decimal.Round(value, 2) != value)
            {
                throw new TooManyDecimalPlacesException();
            }

            _value = value;
        }

        private decimal _value;

        public static implicit operator decimal(Money money) => money._value;
        public static Money operator +(Money lhs, Money rhs) => new(lhs._value + rhs._value);
        public static Money operator -(Money lhs, Money rhs) => new(lhs._value - rhs._value);
    }
}