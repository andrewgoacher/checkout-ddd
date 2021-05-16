using System.Text.Json;
using KataApi.Domain.Infrastructure.Services;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public static class CustomSerialisationOptions
    {
        public static JsonSerializerOptions Get()
        {

            return new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    // todo: This is a bit of a hack just to progress with the work
                    // ideally need to figure out what the right approach would be
                    new BasketConverter(new ItemService()),
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
