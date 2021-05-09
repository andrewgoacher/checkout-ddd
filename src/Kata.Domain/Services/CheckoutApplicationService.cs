using Kata.Domain.Checkout;

namespace Kata.Domain.Services
{
    public class CheckoutApplicationService
    {
        private readonly IItemService _itemService;

        private Basket _basket;

        public CheckoutApplicationService(IItemService itemService)
        {
            _itemService = itemService;
        }

        public BasketId CreateBasket()
        {
            // todo: Remove opportunity for null;

            if (_basket != null)
            {
                throw new BasketExistsException();
            }

            _basket = Basket.Create(_itemService);
            return _basket.Id;
        }
    }
}