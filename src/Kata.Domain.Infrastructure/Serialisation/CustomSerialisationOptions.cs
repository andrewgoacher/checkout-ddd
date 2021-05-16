using System.Text.Json;
using Kata.Domain.Services;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public static class CustomSerialisationOptions
    {
        public static JsonSerializerOptions Get(IItemService itemService)
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new BasketConverter(itemService),
                    new BasketIdConverter(),
                    new DescriptionConverter(),
                    new MoneyConverter(),
                    new PriceConverter(),
                    new QuantityConverter(),
                    new ItemIdConverter(),
                    new ItemConverter(),
                    new DiscountIdConverter(),
                    new DiscountConverter()
                }
            };
        }
    }
}
