using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

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
            Assert.True(CollectionExtensions.ListEquals((List<string>)null, (List<string>)null));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals((List<string>)null));
            Assert.False(CollectionExtensions.ListEquals((List<string>)null, new[] { "a", "b", "c" }.ToList()));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "a", "b" }));
            Assert.False(new[] { "a", "b" }.ToList().ListEquals(new[] { "a", "b", "c" }));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "a", "b", "d" }));
            Assert.True(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "a", "b", "c" }));
            Assert.False(new[] { "a", null, "c" }.ToList().ListEquals(new[] { "a", "b", "c" }));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "a", "b", null }));
            Assert.False(new[] { "a", "b", "c" }.ToList().ListEquals(new[] { "b", "a", "c" }));
        }

        [Fact]
        public void TestDictEquals()
        {
            var dic1 = new Dictionary<string, string> { { "linux", "good" }, { "macos", "bad" }, { "windows", "ugly" } };
            var dic2 = new Dictionary<string, string> { { "linux", "good" }, { "freebsd", "correct" },
                { "macos", "bad" }, { "windows", "ugly" } };
            var dic3 = new Dictionary<string, string> { { "linux", "good" }, { "freebsd", "correct" }, { "windows", "ugly" } };
            var dic4 = new Dictionary<string, string> { { "linux", "good" }, { "macos", "bad" }, { "windows", "awful" } };
            var dic5 = new Dictionary<string, string> { { "linux", "good" }, { "macos", "bad" }, { "windows", null } };
            var dic6 = new Dictionary<string, string> { { "linux", "good" }, { "macos", "bad" }, { "windows", "ugly" } };

            Assert.True(CollectionExtensions.DictEquals((IDictionary<string, string>)null, (IDictionary<string, string>)null));
            Assert.False(CollectionExtensions.DictEquals((IDictionary<string, string>)null, dic1));
            Assert.False(dic1.DictEquals(null));
            Assert.False(dic1.DictEquals(dic2));
            Assert.False(dic1.DictEquals(dic3));
            Assert.False(dic1.DictEquals(dic4));
            Assert.False(dic1.DictEquals(dic5));
            Assert.True(dic1.DictEquals(dic6));
        }
    }
}
