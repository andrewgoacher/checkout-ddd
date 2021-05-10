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
            public string ItemId { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }

        public sealed class AddItemQuantity
        {
            public string ItemId { get; set; }
            public int Quantity { get; set; }
        }

        public sealed class RemoveItemQuantity
        {
            public string ItemId { get; set; }
            public int Quantity { get; set; }
        }

        public sealed class RemoveDiscounts
        {
        }

        public sealed class AddDiscount
        {
            public Guid DiscountId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
    }
}