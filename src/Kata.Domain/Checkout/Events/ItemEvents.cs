using System;

namespace Kata.Domain.Checkout.Events
{
    public static class ItemEvents
    {
        public class NewItem
        {
            public Guid ParentId { get; set; }
            public string ItemId { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }

        public class IncrementQuantity
        {
            public int Amount { get; set; }
        }

        public class DecrementQuantity
        {
            public int Amount { get; set; }
        }
    }
}