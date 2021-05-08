namespace Kata.Domain.Shared
{
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