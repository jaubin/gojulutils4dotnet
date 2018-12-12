using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

namespace Org.Gojul.Extensions.Tests
{
    public enum Foo
    {
        Foo, Bar
    }

    public class StringExtensionsTests
    {
        [Fact]
        public void TestParseEnumWithNullStringThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensions.ParseEnum<Foo>(null));
        }

        [Fact]
        public void TestParseEnum()
        {
            Assert.Equal(Foo.Foo, "Foo".ParseEnum<Foo>());
            Assert.Equal(Foo.Bar, "bar".ParseEnum<Foo>());
            Assert.Throws<ArgumentException>(() => "linux".ParseEnum<Foo>());
        }
    }
}
