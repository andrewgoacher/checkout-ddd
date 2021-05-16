using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Infrastructure.DB;
using Kata.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace KataApi.Domain.Infrastructure.DB
{
    // Note:  Appreciate that this is a pretty sloppy store implementation but that's Ok for now.
    internal class BasketStore : IBasketStore
    {
        private readonly IItemService _itemService;
        private readonly KataContext _context;

        public BasketStore(KataContext context, IItemService itemService)
        {
            _itemService = itemService;
            _context = context;
        }

        public async Task<bool> ExistsAsync(BasketId id)
        {
            var basket = await _context.Baskets.Where(basket => basket.Id == id).SingleOrDefaultAsync();
            return basket != null;
        }

        public async Task<Basket> GetBasketAsync(BasketId id)
        {
            var dto = await _context.Baskets
                .Include(c => c.Items)
                .Include(c => c.Discounts)
                .Where(basket => basket.Id == id).SingleOrDefaultAsync();

            var items = dto.Items?.Select(item => Item.NewItem(id, new(item.ItemId), new(item.Price), new(item.Quantity))) ?? Enumerable.Empty<Item>();

            var basket = Basket.Load(_itemService, id, items);

            var discounts = dto.Discounts ?? Enumerable.Empty<Kata.Domain.Infrastructure.DB.Models.Discount>();
            foreach (var discount in discounts)
            {
                basket.AddDiscount(new(discount.DiscountId), new(discount.Description), new(discount.Amount));
            }

            return basket;
        }

        public async Task StoreBasketAsync(Basket basket)
        {
            if (await ExistsAsync(basket.Id))
            {
                var dto = await _context.Baskets.SingleAsync(x => x.Id == basket.Id);
                dto.Discounts = GetDiscounts();
                dto.Items = GetItems();
            }
            else
            {
                _context.Baskets.Add(new Kata.Domain.Infrastructure.DB.Models.Basket
                {
                    Discounts = GetDiscounts(),
                    Items = GetItems(),
                    Id = basket.Id
                });
            }

            await _context.SaveChangesAsync();

            List<Kata.Domain.Infrastructure.DB.Models.Discount> GetDiscounts() =>
                basket.GetDiscounts().Select(discount => new Kata.Domain.Infrastructure.DB.Models.Discount
                {
                    Amount = discount.Amount,
                    Description = discount.Description,
                    DiscountId = discount.Id,
                    ParentId = discount.ParentId
                }).ToList();

            List<Kata.Domain.Infrastructure.DB.Models.Item> GetItems() =>
                basket.GetItems().Select(item => new Kata.Domain.Infrastructure.DB.Models.Item
                {
                    ItemId = item.Id,
                    ParentId = item.ParentId,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList();
        }
    }
}
