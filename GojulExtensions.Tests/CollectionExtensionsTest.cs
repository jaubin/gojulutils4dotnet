using System;
using Xunit;

using System.Linq;

namespace Org.Gojul.Extensions.Tests
{
    public class CollectionExtensionsTest
    {
        [Fact]
        public void TestSplitWithNullCollectionThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => CollectionExtensions.Split<string>(null, 42));
        }

        [Fact]
        public void TestSplitWithNegativeChunkThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CollectionExtensions.Split(new[] { "foo", "bar" }, -42));
        }

        [Fact]
        public void TestSplitWithEmptyCollectionReturnEmptyCollection()
        {
            var res = new string[] { }.ToList().Split(42);

            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public void TestSplit()
        {
            Assert.Equal(new[] { new[] { 1, 2 }.ToList() }.ToList(), new[] { 1, 2 }.ToList().Split(2));
            Assert.Equal(new[] { new[] { 1, 2 }.ToList(), new[] { 3, 4 }.ToList() }.ToList(), new[] { 1, 2, 3, 4 }.ToList().Split(2));
            Assert.Equal(new[] { new[] { 1, 2 }.ToList(), new[] { 3, 4 }.ToList(), new[] { 5 }.ToList() }.ToList(), new[] { 1, 2, 3, 4, 5 }.ToList().Split(2));

        }
    }
}
