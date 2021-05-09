using System;
namespace Kata.Domain.Checkout.Events
{
    public static class DiscountEvents
    {
        public sealed class NewDiscount
        {
            public Guid Id { get; set; }
            public Guid ParentId { get; set; }
            public string Description { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
