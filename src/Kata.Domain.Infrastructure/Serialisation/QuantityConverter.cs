using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Shared;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class QuantityConverter : JsonConverter<Quantity>
    {
        public QuantityConverter()
        {
        }

        public override Quantity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Quantity(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, Quantity value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
