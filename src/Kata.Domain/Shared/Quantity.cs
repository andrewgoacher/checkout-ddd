namespace Kata.Domain.Shared
{
    public record Quantity
    {
        private readonly int _value;

        public Quantity(int value)
        {
            if (value < 0 )
            {
                throw new InvalidQuantityException();
            }

            _value = value;
        }

        public Quantity Increment()
        {
            return new(_value + 1);
        }

        public Quantity IncrementBy(Quantity amount)
        {
            return new(_value + amount._value);
        }

        public static implicit operator int(Quantity qty) => qty._value;
    }
}
