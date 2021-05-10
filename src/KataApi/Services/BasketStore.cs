using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Services;
using KataApi.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KataApi.Services
{
    // Note:  Appreciate that this is a pretty sloppy store implementation but that's Ok for now.
    public class BasketStore : IBasketStore
    {
        private readonly IMongoCollection<Basket> _basketCollection;

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
            var basket = await baskets.SingleAsync();
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
