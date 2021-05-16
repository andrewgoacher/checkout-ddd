using System;
namespace Kata.Domain.Infrastructure.DB.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public Guid DiscountId { get; set; }
        public Basket Parent { get; set; }
        public Guid ParentId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
