using System;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Services;

namespace Kata.Domain.UnitTests.Services
{
    public class BasketStoreFake : IBasketStore
    {
        private Basket _basket;

        public BasketStoreFake()
        {
        }

        public Task<bool> ExistsAsync(BasketId id)
        {
            return Task.FromResult(_basket != null && id == _basket.Id);
        }

        public Task StoreBasketAsync(Basket basket)
        {
            _basket = basket;
            return Task.CompletedTask;
        }
    }
}
