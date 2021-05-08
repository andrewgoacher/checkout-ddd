using System;
using Kata.Domain.Checkout.Events;
using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class Basket : AggregateRoot<BasketId>
    {
        private Basket() : base()
        {
            
        }

        public static Basket Create()
        {
            var basket = new Basket();
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