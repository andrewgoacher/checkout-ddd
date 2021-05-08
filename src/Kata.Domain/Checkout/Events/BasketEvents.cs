using System;

namespace Kata.Domain.Checkout.Events
{
    public static class BasketEvents
    {
        public sealed class NewBasket
        {
            public Guid Id { get; set; }
        }
    }
}