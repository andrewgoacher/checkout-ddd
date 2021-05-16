using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Shared;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class PriceConverter : JsonConverter<Price>
    {
        public PriceConverter()
        {
        }

        public override Price Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Price(reader.GetDecimal());
        }

        public override void Write(Utf8JsonWriter writer, Price value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
