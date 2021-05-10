using System;
using Kata.Domain.Checkout;

namespace KataApi.Services
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(ItemId itemId) : base($"Item with id ({itemId}) not found")
        {
        }
    }
}
