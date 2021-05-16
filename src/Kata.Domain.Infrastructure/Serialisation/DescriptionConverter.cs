using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Shared;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class DescriptionConverter : JsonConverter<Description>
    {
        public DescriptionConverter()
        {
        }

        public override Description Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Description(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Description value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
