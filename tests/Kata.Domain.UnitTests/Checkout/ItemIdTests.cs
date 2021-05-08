using Kata.Domain.Checkout;
using Xunit;

namespace Kata.Domain.UnitTests.Checkout
{
    public class ItemIdTests
    {
        [Fact]
        public void ItemId_NullString_ThrowsInvalidItemIdException()
        {
            Assert.Throws<InvalidItemIdException>(() =>
            {
                new ItemId(null);
            });
        }

        [Fact]
        public void ItemId_EmptyString_ThrowsInvalidItemIdException()
        {
            Assert.Throws<InvalidItemIdException>(() =>
            {
                new ItemId("");
            });
        }

        [Fact]
        public void ItemId_ValidId_ContainsCorrectId()
        {
            var id = "Item Id";
            var itemId = new ItemId(id);
            
            Assert.Equal(id, itemId);
        }

        [Fact]
        public void ItemId_SameId_AreEqual()
        {
            var id = "Item Id";

            var id1 = new ItemId(id);
            var id2 = new ItemId(id);
            
            Assert.Equal(id1, id2);
        }

        [Fact]
        public void ItemId_DifferentId_AreNotEqual()
        {
            var id1 = new ItemId("Item Id 1");
            var id2 = new ItemId("Item Id 2");
            
            Assert.NotEqual(id1, id2);
        }
    }
}