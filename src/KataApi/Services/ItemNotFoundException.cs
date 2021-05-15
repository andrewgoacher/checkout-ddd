using System;
using Kata.Domain.Checkout;

namespace KataApi.Services
{
    /// <summary>
    /// Thrown when an item key does not exist
    /// </summary>
    public class ItemNotFoundException : Exception
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
