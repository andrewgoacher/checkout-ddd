using Kata.Domain.Checkout;
using Kata.Domain.Core;

namespace KataApi.Domain.Infrastructure.Services
{
    /// <summary>
    /// Thrown when an item key does not exist
    /// </summary>
    public class ItemNotFoundException : EntityNotFoundException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId">The <see cref="ItemId"/> requested</param>
        public ItemNotFoundException(ItemId itemId) : base($"Item with id ({itemId}) not found")
        {
        }
    }
}
