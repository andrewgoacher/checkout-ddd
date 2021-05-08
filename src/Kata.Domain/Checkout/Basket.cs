using System;
using Kata.Domain.Checkout.Events;
using Kata.Domain.Core;

namespace Kata.Domain.Checkout
{
    public class Basket : AggregateRoot
    {
        private Basket() : base()
        {
            
        }

        public static Basket Create()
        {
            var basket = new Basket();
            basket.Apply(new NewBasket
            {
                Id = Guid.NewGuid()
            });
            return basket;
        }
        
        public BasketId BasketId { get; private set; }
        
        protected override void OnApply(object @event)
        {
            switch (@event)
            {
                case NewBasket nb:
                {
                    BasketId = new BasketId(nb.Id);
                    break;
                }
            }
        }

        protected override void EnsureValidState()
        {
            if (BasketId == null)
            {
                throw new InvalidBasketException("BasketId");
            }
        }
    }
}