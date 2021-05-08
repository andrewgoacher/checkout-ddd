using Kata.Domain.Checkout;
using Kata.Domain.Shared;

namespace Kata.Domain.Services.Models
{
    public class Item
    {
        public ItemId Id { get; set; }
        public Price Price { get; set; }
    }
}