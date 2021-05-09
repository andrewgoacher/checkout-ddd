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

        public static Item NewItem(Basket basket, ItemId id, Price price)
        {
            var item = new Item();
            item.Apply(new ItemEvents.NewItem()
            {
                ParentId = basket.Id,
                Price = price,
                ItemId = id
            });
            return item;
        }
        
        public BasketId ParentId { get; private set; }
        
        public Price Price { get; private set; }

        protected override void OnApply(object @event)
        {
            switch (@event)
            {
                case ItemEvents.NewItem ni:
                {
                    ParentId = new BasketId(ni.ParentId);
                    Id = new ItemId(ni.ItemId);
                    Price = new Price(ni.Price);
                    break;
                }
            }
        }
    }
}