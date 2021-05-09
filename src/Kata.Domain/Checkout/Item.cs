using Kata.Domain.Checkout.Events;
using Kata.Domain.Core;
using Kata.Domain.Shared;

namespace Kata.Domain.Checkout
{
    public class Item : Entity<ItemId>
    {
        private Item() : base()
        {
        }

        public static Item NewItem(Basket basket, ItemId id, Price price, Quantity qty)
        {
            var item = new Item();
            item.Apply(new ItemEvents.NewItem()
            {
                ParentId = basket.Id,
                Price = price,
                ItemId = id,
                Quantity = qty
            });
            return item;
        }

        public BasketId ParentId { get; private set; }

        public Price Price { get; private set; }

        public Quantity Quantity { get; private set; }

        protected override void OnApply(object @event)
        {
            switch (@event)
            {
                case ItemEvents.NewItem ni:
                    {
                        if (ni.Quantity == 0)
                        {
                            throw new InvalidItemQuantityException();
                        }

                        ParentId = new BasketId(ni.ParentId);
                        Id = new ItemId(ni.ItemId);
                        Price = new Price(ni.Price);
                        Quantity = new Quantity(ni.Quantity);
                        break;
                    }
            }
        }
    }
}