using System.Threading.Tasks;
using Kata.Domain.Checkout;

namespace Kata.Domain.Services
{
    using Models;
    
    public interface IItemService
    {
        Task<Item> FetchItemAsync(ItemId itemId);
    }
}