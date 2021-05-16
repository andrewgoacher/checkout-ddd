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
        private readonly List<Discount> _discounts;

        public event EventHandler<BasketEvents.NewBasket> NewBasketCreated;

        public event EventHandler<BasketEvents.AddItem> ItemAdded;

        public event EventHandler<(ItemId, Quantity)> ItemQuantityUpdated;

        public event EventHandler<ItemId> ItemRemoved;

        public Basket(IItemService itemService) : base()
        {
            _itemService = itemService;

            _items = new List<Item>();
            _discounts = new List<Discount>();
        }

        public IEnumerable<Item> GetItems() => _items.AsEnumerable();

        public IEnumerable<Discount> GetDiscounts() => _discounts.AsEnumerable();

        public Money GetTotal() => new Money(_items.Sum(x => x.Price * x.Quantity) - _discounts.Sum(x => x.Amount));

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
                if (item == null)
                {
                    throw new ItemNotFoundException($"The itemwith id ({itemId}) could not be found");
                }

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

        public static Basket Load(IItemService itemService, BasketId id, IEnumerable<Item> items)
        {
            var basket = new Basket(itemService);
            basket.Apply(new BasketEvents.NewBasket
            {
                Id = id
            });
            foreach (var item in items)
            {
                basket.Apply(new BasketEvents.AddItem
                {
                    ItemId = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }
            return basket;
        }

        public void ClearDiscounts()
        {
            Apply(new BasketEvents.RemoveDiscounts());
        }

        public void AddDiscount(DiscountId id, Description desc, Money amount)
        {
            Apply(new BasketEvents.AddDiscount
            {
                Amount = amount,
                Description = desc,
                DiscountId = id
            });
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
                        _items.Add(Item.NewItem(this.Id, new(ai.ItemId), new(ai.Price), new(ai.Quantity)));
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
                case BasketEvents.RemoveDiscounts:
                    {
                        _discounts.Clear();
                        break;
                    }
                case BasketEvents.AddDiscount d:
                    {
                        _discounts.Add(Discount.NewDiscount(this.Id, new(d.DiscountId), new(d.Description), new(d.Amount)));
                        break;
                    }
            }
        }

        protected override void EnsureValidState()
        {
            if (Id == null)
            {
                throw new InvalidBasketException("Id", "null");
            }

            if (_items.Any(x => x.Quantity == 0))
            {
                throw new InvalidBasketException("Empty Item");
            }

            if (!_items.Any() && _discounts.Any())
            {
                throw new InvalidBasketException("No items to apply discount");
            }
        }
    }
}