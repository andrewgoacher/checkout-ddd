using System;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Services;

namespace KataApi.Services
{
    public class ItemService : IItemService
    {
        public ItemService()
        {
        }

        public Task<Kata.Domain.Services.Models.Item> FetchItemAsync(ItemId itemId)
        {
            throw new NotImplementedException();
        }
    }
}
