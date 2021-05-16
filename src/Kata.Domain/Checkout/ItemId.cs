namespace Kata.Domain.Checkout
{

    [System.Diagnostics.DebuggerDisplay("{id}")]
    public record ItemId
    {
        public ItemId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidItemIdException();
            }

            _id = id;
        }

        private string _id;

        public static implicit operator string(ItemId id) => id._id;

        public override string ToString()
        {
            return _id;
        }
    }
}