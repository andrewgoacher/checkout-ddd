using System;
namespace Kata.Domain.Infrastructure.DB.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemId { get; set; }
        public Basket Parent { get; set; }
        public Guid ParentId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
