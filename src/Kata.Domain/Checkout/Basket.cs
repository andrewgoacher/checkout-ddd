using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kata.Domain.Checkout.Events;
using Kata.Domain.Core;
using Kata.Domain.Services;
using Kata.Domain.Shared;

namespace Kata.Domain.Checkout
{
    public class Basket : AggregateRoot<BasketId>
    {
        private readonly IItemService _itemService;
        private readonly List<Item> _items;

        public event EventHandler<BasketEvents.NewBasket> NewBasketCreated;
        public event EventHandler<BasketEvents.AddItem> ItemAdded;

        private Basket(IItemService itemService) : base()
        {
            _itemService = itemService;
            
            _items = new List<Item>();
        }

        public IEnumerable<Item> GetItems() => _items.AsEnumerable();

        public Money GetTotal() => new Money(_items.Sum(x => x.Price));

        public async Task AddItemAsync(ItemId itemId)
        {
            var item = await _itemService.FetchItemAsync(itemId);
            Apply(new BasketEvents.AddItem()
            {
                ItemId = itemId,
                Price = item.Price
            });
        }

        public static Basket Create(IItemService itemService)
        {
            var basket = new Basket(itemService);
            basket.Apply(new BasketEvents.NewBasket
            {
                Id = Guid.NewGuid()
            });
            return basket;
        }
        
        protected override void OnApply(object @event)
        {
            switch (@event)
            {
                case BasketEvents.NewBasket nb:
                {
                    Id = new BasketId(nb.Id);
                    NewBasketCreated?.Invoke(this, nb);
                    break;
                }
                case BasketEvents.AddItem ai:
                {
                    _items.Add(Item.NewItem(this, new (ai.ItemId), new (ai.Price)));
                     ItemAdded?.Invoke(this, ai);
                    break;
                }
            }
        }

        protected override void EnsureValidState()
        {
            if (Id == null)
            {
                throw new InvalidBasketException("Id");
            }
        }
    }
}