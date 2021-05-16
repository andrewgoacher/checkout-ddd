using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Checkout;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class DiscountIdConverter : JsonConverter<DiscountId>
    {
        public DiscountIdConverter()
        {
        }

        public override DiscountId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var guid = Guid.Parse(reader.GetString());
            return new DiscountId(guid);
        }

        public override void Write(Utf8JsonWriter writer, DiscountId value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
