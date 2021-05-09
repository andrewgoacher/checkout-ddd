using System;
using Kata.Domain.Core;
using Kata.Domain.Shared;

namespace Kata.Domain.Checkout
{
    public class Discount : Entity<DiscountId>
    {
        private Discount()
        {
        }

        public BasketId ParentId { get; private set; }
        public Money Amount { get; private set; }

        protected override void OnApply(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
