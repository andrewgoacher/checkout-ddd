using System.Collections.Generic;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Services;

namespace KataApi.Services
{
    using Item = Kata.Domain.Services.Models.Item;

    public class ItemService : IItemService
    {
        private readonly Dictionary<ItemId, Item> _items;

        public ItemService()
        {
            _items = new()
            {
                { new("A"), new() { Id = new("A"), Price = new(50) } },
                { new("B"), new() { Id = new("B"), Price = new(30) } },
                { new("C"), new() { Id = new("C"), Price = new(20) } },
                { new("D"), new() { Id = new("D"), Price = new(15) } }
            };
        }

        public Task<Item> FetchItemAsync(ItemId itemId)
        {
            if (_items.TryGetValue(itemId, out var item))
            {
                return Task.FromResult(item);
            }

            throw new ItemNotFoundException(itemId);
        }
    }
}
