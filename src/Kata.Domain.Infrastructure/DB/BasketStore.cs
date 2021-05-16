using System;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Core;
using Kata.Domain.Services;
using Kata.Domain.Shared;
using KataApi.Domain.Infrastructure.Config;
using Microsoft.Extensions.Options;

namespace KataApi.Domain.Infrastructure.DB
{
    // Note:  Appreciate that this is a pretty sloppy store implementation but that's Ok for now.
    public class BasketStore : IBasketStore
    {

        public BasketStore(IOptions<BasketStoreSettings> settings)
        {

        }

        public async Task<bool> ExistsAsync(BasketId id)
        {
            //var baskets = await _basketCollection.FindAsync(x => x.Id == id);
            //var basket = await baskets.SingleOrDefaultAsync();
            //return basket != null;
            return false;
        }

        public async Task<Basket> GetBasketAsync(BasketId id)
        {
            //var baskets = await _basketCollection.FindAsync(x => x.Id == id);
            //var basket = await baskets.SingleOrDefaultAsync();
            //return basket;
            return null;
        }

        public async Task StoreBasketAsync(Basket basket)
        {
            if (await ExistsAsync(basket.Id))
            {
                //await _basketCollection.ReplaceOneAsync(x => x.Id == basket.Id, basket);
            }
            else
            {
                //await _basketCollection.InsertOneAsync(basket);
            }
        }
    }
}
