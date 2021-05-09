using System;
using System.Threading.Tasks;
using Kata.Domain.Checkout;

namespace Kata.Domain.Services
{
    public interface IBasketStore
    {
        Task StoreBasketAsync(Basket basket);
        Task<bool> ExistsAsync(BasketId id);
        Task<Basket> GetBasketAsync(BasketId id);
    }
}
