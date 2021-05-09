using System;
using System.Collections.Generic;
using System.Linq;
using Kata.Domain.Checkout;

namespace Kata.Domain.Services.Models
{
    using CheckoutItem = Kata.Domain.Checkout.Item;

    public abstract class DiscountRule
    {
        public abstract IEnumerable<Discount> Apply(IEnumerable<CheckoutItem> items);
    }

    public class ThreeAFor130DiscountRule : DiscountRule
    {
        public override IEnumerable<Discount> Apply(IEnumerable<CheckoutItem> items)
        {
            var item = items.SingleOrDefault(x => x.Id == new ItemId("A"));
            if (item == null)
            {
                return Enumerable.Empty<Discount>();
            }
            var count = item.Quantity / 3;
            return Enumerable.Range(0, count).Select(x => new Discount
            {
                Amount = new Shared.Money(20),
                Description = new Shared.Description("3 for 130"),
                Id = new DiscountId(Guid.NewGuid())
            });
        }
    }

    public class TwoBFor45DiscountRule : DiscountRule
    {
        public override IEnumerable<Discount> Apply(IEnumerable<CheckoutItem> items)
        {
            var item = items.SingleOrDefault(x => x.Id == new ItemId("B"));
            if (item == null)
            {
                return Enumerable.Empty<Discount>();
            }
            var count = item.Quantity / 2;
            return Enumerable.Range(0, count).Select(x => new Discount
            {
                Amount = new Shared.Money(15),
                Description = new Shared.Description("2 for 45"),
                Id = new DiscountId(Guid.NewGuid())
            });
        }
    }
}
