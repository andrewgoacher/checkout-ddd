namespace Kata.Domain.Shared
{

    [System.Diagnostics.DebuggerDisplay("{_value}")]
    public record Price : Money
    {
        public Price(decimal value) : base(value)
        {
            if (value <= 0m)
            {
                throw new InvalidPriceException();
            }
        }
    }
}