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
        public event EventHandler<(ItemId, Quantity)> ItemQuantityUpdated;
        public event EventHandler<ItemId> ItemRemoved;

        private Basket(IItemService itemService) : base()
        {
            _itemService = itemService;

            _items = new List<Item>();
        }

        public IEnumerable<Item> GetItems() => _items.AsEnumerable();

        public Money GetTotal() => new Money(_items.Sum(x => x.Price * x.Quantity));

        public async Task AddItemAsync(ItemId itemId, Quantity qty)
        {
            var exists = _items.Exists(x => x.Id == itemId);

            if (exists)
            {
                Apply(new BasketEvents.AddItemQuantity()
                {
                    ItemId = itemId,
                    Quantity = qty
                });
            }
            else
            {

                var item = await _itemService.FetchItemAsync(itemId);
                Apply(new BasketEvents.AddItem()
                {
                    ItemId = itemId,
                    Price = item.Price,
                    Quantity = qty
                });
            }
        }

        public void RemoveItem(ItemId itemId, Quantity quantity)
        {
            Apply(new BasketEvents.RemoveItemQuantity
            {
                ItemId = itemId,
                Quantity = quantity
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
                        _items.Add(Item.NewItem(this, new(ai.ItemId), new(ai.Price), new(ai.Quantity)));
                        ItemAdded?.Invoke(this, ai);
                        break;
                    }
                case BasketEvents.AddItemQuantity aiq:
                    {
                        var item = _items.Single(x => x.Id == aiq.ItemId);
                        item.IncrementQuantity(new(aiq.Quantity));
                        ItemQuantityUpdated?.Invoke(this, (item.Id, item.Quantity));
                        break;
                    }
                case BasketEvents.RemoveItemQuantity ri:
                    {
                        var item = _items.Single(x => x.Id == ri.ItemId);
                        item.DecrementQuantity(new(ri.Quantity));
                        if (item.Quantity == 0)
                        {
                            _items.Remove(item);
                            ItemRemoved?.Invoke(this, item.Id);
                        }
                        else
                        {
                            ItemQuantityUpdated?.Invoke(this, (item.Id, item.Quantity));
                        }
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

            if (_items.Any(x => x.Quantity == 0))
            {
                throw new InvalidBasketException("Empty Item");
            }
        }
    }
}