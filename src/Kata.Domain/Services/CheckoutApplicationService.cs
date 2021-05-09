using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Shared;

namespace Kata.Domain.Services
{
    public class CheckoutApplicationService
    {
        private readonly IItemService _itemService;
        private readonly IBasketStore _basketStore;

        public CheckoutApplicationService(IItemService itemService, IBasketStore basketStore)
        {
            _itemService = itemService;
            _basketStore = basketStore;
        }

        public async Task<BasketId> CreateBasket()
        {
            var basket = Basket.Create(_itemService);
            await _basketStore.StoreBasketAsync(basket);
            return basket.Id;
        }

        public async Task AddItemAsync(BasketId basketId, ItemId itemId, Quantity qty)
        {
            if (!await _basketStore.ExistsAsync(basketId))
            {
                throw new BasketNotFoundException();
            }

            var basket = await _basketStore.GetBasketAsync(basketId);
            await basket.AddItemAsync(itemId, qty);
            await _basketStore.StoreBasketAsync(basket);
        }

        public async Task RemoveItemAsync(BasketId basketId, ItemId itemId, Quantity qty)
        {
            if (!await _basketStore.ExistsAsync(basketId))
            {
                throw new BasketNotFoundException();
            }

            var basket = await _basketStore.GetBasketAsync(basketId);
            basket.RemoveItem(itemId, qty);
            await _basketStore.StoreBasketAsync(basket);
        }
    }
}