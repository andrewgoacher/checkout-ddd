namespace Kata.Domain.Shared
{
    public record Money
    {
        public Money(decimal value)
        {
            if (decimal.Round(value, 2) != value)
            {
                throw new TooManyDecimalPlacesException();
            }

            Value = value;
        }
        
        public decimal Value { get; }

        public static implicit operator decimal(Money money) => money.Value;
        public static Money operator +(Money lhs, Money rhs) => new(lhs.Value + rhs.Value);
        public static Money operator -(Money lhs, Money rhs) => new(lhs.Value - rhs.Value);
    }
}