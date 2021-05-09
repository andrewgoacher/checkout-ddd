using System;
using Kata.Domain.Shared;
using Xunit;

namespace Kata.Domain.UnitTests.Shared
{
    public class DescriptionTests
    {
        public DescriptionTests()
        {
        }

        [Fact]
        public void Description_NullString_ThrowsInvalidDescriptionException()
        {
            Assert.Throws<InvalidDescriptionException>(() =>
            {
                new Description(null);
            });
        }

        [Fact]
        public void Description_EmptyString_ThrowsInvalidDescriptionException()
        {
            Assert.Throws<InvalidDescriptionException>(() =>
            {
                new Description("");
            });
        }

        [Fact]
        public void Description_ValidDescription_HasCorrectDescription()
        {
            var descriptionText = "This is a test description";

            var description = new Description(descriptionText);

            Assert.Equal(descriptionText, description);
        }
    }
}
