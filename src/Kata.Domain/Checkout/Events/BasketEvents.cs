using System;

namespace Kata.Domain.Checkout.Events
{
    public static class BasketEvents
    {
        public sealed class NewBasket
        {
            public Guid Id { get; set; }
        }
        
        public sealed class AddItem
        {
            public string ItemId {get;set; }
            public decimal Price {get; set;}
        }
    }
}