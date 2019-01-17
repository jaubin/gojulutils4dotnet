using System;
using Xunit;

using System.Linq;
using System.Collections.Generic;

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

        [Fact]
        public void TestListEquals()
        {
            Assert.True(CollectionExtensions.ListEquals((List<string>) null, (List<string>) null));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals( (List<string>)null));
            Assert.False(CollectionExtensions.ListEquals((List<string>)null, new[] { "a", "b", "c" }.ToList()));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "a", "b" }));
            Assert.False(new[] { "a", "b" }.ToList().ListEquals(new[] { "a", "b", "c" }));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "a", "b", "d" }));
            Assert.True(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "a", "b", "c" }));
            Assert.False(new[] { "a", null, "c" }.ToList().ListEquals(new[] { "a", "b", "c" }));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "a", "b", null }));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "b", "a", "c" }));
        }
    }
}
