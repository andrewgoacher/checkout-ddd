using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Checkout;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class BasketIdConverter : JsonConverter<BasketId>
    {
        public BasketIdConverter()
        {
        }

        public override BasketId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var guid = Guid.Parse(reader.GetString());
            return new BasketId(guid);
        }

        public override void Write(Utf8JsonWriter writer, BasketId value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
