using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Shared;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class MoneyConverter : JsonConverter<Money>
    {
        public MoneyConverter()
        {
        }

        public override Money Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Money(reader.GetDecimal());
        }

        public override void Write(Utf8JsonWriter writer, Money value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
