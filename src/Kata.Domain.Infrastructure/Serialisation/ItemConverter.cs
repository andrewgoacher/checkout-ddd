using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Checkout;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class ItemConverter : JsonConverter<Item>
    {
        public ItemConverter()
        {
        }

        public override Item Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string id = "";
            string parentId = "";
            decimal price = 0m;
            int quantity = 0;

            string propertyName = "";

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return Item.NewItem(
                        new(Guid.Parse(parentId)),
                        new(id),
                        new(price),
                        new(quantity));
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
                        case "Price":
                            price = reader.GetDecimal();
                            break;
                        case "Quantity":
                            quantity = reader.GetInt32();
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Item value, JsonSerializerOptions options)
        {
            writer.WriteString("Id", value.Id);
            writer.WriteString("ParentId", value.ParentId);
            writer.WriteNumber("Price", value.Price);
            writer.WriteNumber("Quantity", value.Quantity);
        }
    }
}
