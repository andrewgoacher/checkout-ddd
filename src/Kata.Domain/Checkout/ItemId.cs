namespace Kata.Domain.Checkout
{
    public record ItemId
    {
        public ItemId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidItemIdException();
            }

            Id = id;
        }
        
        public string Id { get; }

        public static implicit operator string(ItemId id) => id.Id;
    }
}