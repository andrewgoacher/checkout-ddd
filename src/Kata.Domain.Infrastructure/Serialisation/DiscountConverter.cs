using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Checkout;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class DiscountConverter : JsonConverter<Discount>
    {
        public DiscountConverter()
        {
        }

        public override Discount Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string id = "";
            string parentId = "";
            decimal amount = 0m;
            string description = "";

            string propertyName = "";

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return Discount.NewDiscount(
                        new(Guid.Parse(parentId)),
                        new(Guid.Parse(id)),
                        new(description),
                        new(amount));
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "ParentId":
                            parentId = reader.GetString();
                            break;
                        case "Id":
                            id = reader.GetString();
                            break;
                        case "Description":
                            description = reader.GetString();
                            break;
                        case "Amount":
                            amount = reader.GetDecimal();
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Discount value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("ParentId", value.ParentId);
            writer.WriteString("Description", value.Description);
            writer.WriteNumber("Amount", value.Amount);
            writer.WriteString("Id", value.Id);

            writer.WriteEndObject();
        }
    }
}
