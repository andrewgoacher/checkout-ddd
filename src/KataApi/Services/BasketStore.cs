using System;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Services;

namespace KataApi.Services
{
    public class BasketStore : IBasketStore
    {
        public BasketStore()
        {
        }

        public Task<bool> ExistsAsync(BasketId id)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> GetBasketAsync(BasketId id)
        {
            throw new NotImplementedException();
        }

        public Task StoreBasketAsync(Basket basket)
        {
            throw new NotImplementedException();
        }
    }
}
