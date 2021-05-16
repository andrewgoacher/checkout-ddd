using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Checkout;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class ItemIdConverter : JsonConverter<ItemId>
    {
        public ItemIdConverter()
        {
        }

        public override ItemId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new ItemId(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, ItemId value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
