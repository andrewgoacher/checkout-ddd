using System.Collections.Generic;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Services;

namespace Kata.Domain.UnitTests.Services
{
    using Kata.Domain.Services.Models;
    
    public class ItemServiceStub : IItemService
    {
        private readonly Dictionary<ItemId, Item> _items;

        public ItemServiceStub()
        {
            _items = new ()
            {
                {new ("A"), new () { Id=new("A"), Price = new (50) }},
                {new ("B"), new () { Id=new("B"), Price = new (30) }},
                {new ("C"), new () { Id=new("C"), Price = new (20) }},
                {new ("D"), new () { Id=new("D"), Price = new (15) }}
            };
        }

        public Task<Item> FetchItemAsync(ItemId itemId)
        {
            return Task.FromResult(_items[itemId]);
        }
    }
}