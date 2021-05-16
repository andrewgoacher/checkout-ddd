using System;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Core;
using Kata.Domain.Services;
using Kata.Domain.Shared;
using KataApi.Domain.Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace KataApi.Domain.Infrastructure.DB
{
    // Note:  Appreciate that this is a pretty sloppy store implementation but that's Ok for now.
    public class BasketStore : IBasketStore
    {
        private readonly IMongoCollection<Basket> _basketCollection;

        public static void RegisterClasses(IItemService itemService)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Description)))
            {
                BsonClassMap.RegisterClassMap<Description>(d =>
                {
                    d.MapField("_value");
                });
            }


            if (!BsonClassMap.IsClassMapRegistered(typeof(Quantity)))
            {
                BsonClassMap.RegisterClassMap<Quantity>(d =>
                {
                    d.MapField("_value");
                });
            }


            if (!BsonClassMap.IsClassMapRegistered(typeof(Money)))
            {
                BsonClassMap.RegisterClassMap<Money>(d =>
                {
                    d.MapField("_value");
                });
            }


            if (!BsonClassMap.IsClassMapRegistered(typeof(Price)))
            {
                BsonClassMap.RegisterClassMap<Price>(d =>
                {
                    d.AutoMap();
                });
            }


            if (!BsonClassMap.IsClassMapRegistered(typeof(ItemId)))
            {
                BsonClassMap.RegisterClassMap<ItemId>(d =>
                {
                    d.AutoMap();
                    d.MapIdField("_id");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity<ItemId>)))
            {
                BsonClassMap.RegisterClassMap<Entity<ItemId>>(d =>
                {
                    d.AutoMap();
                    d.MapIdProperty<ItemId>(x => x.Id);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Item)))
            {
                BsonClassMap.RegisterClassMap<Item>(d =>
                {
                    d.AutoMap();
                });
            }


            if (!BsonClassMap.IsClassMapRegistered(typeof(DiscountId)))
            {
                BsonClassMap.RegisterClassMap<DiscountId>(d =>
                {
                    d.AutoMap();
                    d.MapIdField("_id");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity<DiscountId>)))
            {
                BsonClassMap.RegisterClassMap<Entity<DiscountId>>(d =>
                {
                    d.AutoMap();
                    d.MapIdProperty<DiscountId>(x => x.Id);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Discount)))
            {
                BsonClassMap.RegisterClassMap<Discount>(d =>
                {
                    d.AutoMap();
                });
            }


            if (!BsonClassMap.IsClassMapRegistered(typeof(BasketId)))
            {
                BsonClassMap.RegisterClassMap<BasketId>(d =>
                {
                    d.AutoMap();
                    d.MapIdField("_id");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(AggregateRoot<BasketId>)))
            {
                BsonClassMap.RegisterClassMap<AggregateRoot<BasketId>>(d =>
                {
                    d.AutoMap();
                    d.MapIdProperty<BasketId>(x => x.Id);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Basket)))
            {
                BsonClassMap.RegisterClassMap<Basket>(d =>
                {
                    d.AutoMap();
                    d.MapCreator(x => new Basket(itemService));
                });
            }
        }

        public BasketStore(IOptions<BasketStoreSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _basketCollection = database.GetCollection<Basket>(settings.Value.CollectionName);
        }

        public async Task<bool> ExistsAsync(BasketId id)
        {
            var baskets = await _basketCollection.FindAsync(x => x.Id == id);
            var basket = await baskets.SingleOrDefaultAsync();
            return basket != null;
        }

        public async Task<Basket> GetBasketAsync(BasketId id)
        {
            var baskets = await _basketCollection.FindAsync(x => x.Id == id);
            var basket = await baskets.SingleOrDefaultAsync();
            return basket;
        }

        public async Task StoreBasketAsync(Basket basket)
        {
            if (await ExistsAsync(basket.Id))
            {
                await _basketCollection.ReplaceOneAsync(x => x.Id == basket.Id, basket);
            }
            else
            {
                await _basketCollection.InsertOneAsync(basket);
            }
        }
    }
}
