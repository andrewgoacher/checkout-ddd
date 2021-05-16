using Kata.Domain.Checkout.Events;
using Kata.Domain.Core;
using Kata.Domain.Shared;

namespace Kata.Domain.Checkout
{
    public class Discount : Entity<DiscountId>
    {
        private Discount()
        {
        }

        public static Discount NewDiscount(BasketId parent, DiscountId id, Description desc, Money amount)
        {
            var discount = new Discount();
            discount.Apply(new DiscountEvents.NewDiscount
            {
                Id = id,
                Description = desc,
                Amount = amount,
                ParentId = parent
            });
            return discount;
        }

        public BasketId ParentId { get; private set; }
        public Money Amount { get; private set; }
        public Description Description { get; private set; }

        protected override void OnApply(object @event)
        {
            switch (@event)
            {
                case DiscountEvents.NewDiscount nd:
                    {
                        ParentId = new BasketId(nd.ParentId);
                        Id = new DiscountId(nd.Id);
                        Amount = new Money(nd.Amount);
                        Description = new Description(nd.Description);
                        break;
                    }
            }
        }
    }
}
