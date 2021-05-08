using System;

namespace Kata.Domain.Checkout.Events
{
    public sealed class NewBasket
    {
        public Guid Id { get; set; }
    }
}