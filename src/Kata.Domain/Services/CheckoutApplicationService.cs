using System.Linq;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Shared;

namespace Kata.Domain.Services
{
    public class CheckoutApplicationService
    {
        private readonly IItemService _itemService;
        private readonly IBasketStore _basketStore;
        private readonly DiscountRuleService _discountRuleService;

        public CheckoutApplicationService(IItemService itemService, IBasketStore basketStore, DiscountRuleService discountRuleService)
        {
            _itemService = itemService;
            _basketStore = basketStore;
            _discountRuleService = discountRuleService;
        }

        public async Task<BasketId> CreateBasket()
        {
            var basket = Basket.Create(_itemService);
            await _basketStore.StoreBasketAsync(basket);
            return basket.Id;
        }

        public async Task<Basket> GetBasket(BasketId basketId)
        {
            var basket = await _basketStore.GetBasketAsync(basketId);
            if (basket == null)
            {
                throw new BasketNotFoundException(basketId);
            }

            return basket;
        }

        public async Task AddItemAsync(BasketId basketId, ItemId itemId, Quantity qty)
        {
            if (!await _basketStore.ExistsAsync(basketId))
            {
                throw new BasketNotFoundException(basketId);
            }

            var basket = await _basketStore.GetBasketAsync(basketId);
            SetupHandlers(basket);
            await basket.AddItemAsync(itemId, qty);
            await _basketStore.StoreBasketAsync(basket);
            RemoveHandlers(basket);
        }

        public async Task RemoveItemAsync(BasketId basketId, ItemId itemId, Quantity qty)
        {
            if (!await _basketStore.ExistsAsync(basketId))
            {
                throw new BasketNotFoundException(basketId);
            }

            var basket = await _basketStore.GetBasketAsync(basketId);
            SetupHandlers(basket);
            basket.RemoveItem(itemId, qty);
            await _basketStore.StoreBasketAsync(basket);
            RemoveHandlers(basket);
        }

        private void SetupHandlers(Basket basket)
        {
            basket.ItemAdded += Basket_ItemAdded;
            basket.ItemQuantityUpdated += Basket_ItemQuantityUpdated;
            basket.ItemRemoved += Basket_ItemRemoved;
        }

        private void RemoveHandlers(Basket basket)
        {
            basket.ItemAdded -= Basket_ItemAdded;
            basket.ItemQuantityUpdated -= Basket_ItemQuantityUpdated;
            basket.ItemRemoved -= Basket_ItemRemoved;
        }

        private void ApplyDiscounts(Basket basket)
        {
            var discounts = _discountRuleService.ApplyDiscounts(basket.GetItems());
            if (discounts.Any())
            {
                basket.ClearDiscounts();
                foreach (var discount in discounts)
                {
                    basket.AddDiscount(discount.Id, discount.Description, discount.Amount);
                }
            }
        }

        private void Basket_ItemRemoved(object sender, ItemId e)
        {
            var basket = (Basket)sender;
            ApplyDiscounts(basket);
        }

        private void Basket_ItemQuantityUpdated(object sender, (ItemId, Quantity) e)
        {
            var basket = (Basket)sender;
            ApplyDiscounts(basket);
        }

        private void Basket_ItemAdded(object sender, Checkout.Events.BasketEvents.AddItem e)
        {
            var basket = (Basket)sender;
            ApplyDiscounts(basket);
        }
    }
}