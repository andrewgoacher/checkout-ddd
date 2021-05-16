using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kata.Domain.Checkout;
using Kata.Domain.Services;

namespace Kata.Domain.Infrastructure.Serialisation
{
    public class BasketConverter : JsonConverter<Basket>
    {
        private readonly IItemService _itemService;

        public BasketConverter(IItemService itemService)
        {
            _itemService = itemService;
        }

        public override Basket Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string id = "";
            List<Discount> discounts = new List<Discount>();
            List<Item> items = new List<Item>();

            string propertyName = "";

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    var basket = Basket.Load(_itemService, new(Guid.Parse(id)), items);
                    foreach (var item in discounts)
                    {
                        basket.AddDiscount(item.Id, item.Description, item.Amount);
                    }
                    return basket;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "Id":
                            id = reader.GetString();
                            break;
                        case "Total":
                            // ignore this.
                            reader.GetString();
                            break;
                        case "Items":
                        case "Discounts":
                            {
                                if (reader.TokenType == JsonTokenType.StartArray)
                                {

                                    while (reader.Read())
                                    {
                                        if (reader.TokenType == JsonTokenType.EndArray)
                                            break;

                                        switch (propertyName)
                                        {
                                            case "Discounts":
                                                {
                                                    discounts.Add(JsonSerializer.Deserialize<Discount>(ref reader, options));
                                                    break;
                                                }
                                            case "Items":
                                                {
                                                    items.Add(JsonSerializer.Deserialize<Item>(ref reader, options));
                                                    break;
                                                }
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Basket value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("Id", value.Id);
            writer.WriteNumber("Total", value.GetTotal());
            writer.WriteStartArray("Items");
            foreach (var item in value.GetItems())
            {
                JsonSerializer.Serialize(writer, item, typeof(Item), options);
            }
            writer.WriteEndArray();
            writer.WriteStartArray("Discounts");
            foreach (var item in value.GetDiscounts())
            {
                JsonSerializer.Serialize(writer, item, typeof(Discount), options);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
