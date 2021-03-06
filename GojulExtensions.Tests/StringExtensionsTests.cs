﻿using System;

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
        public void TestParseEnumWithInvalidFigureThrowsException()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.ParseEnum<Foo>("67"));
        }

        [Fact]
        public void TestParseEnum()
        {
            Assert.Equal(Foo.Foo, "Foo".ParseEnum<Foo>());
            Assert.Equal(Foo.Bar, "bar".ParseEnum<Foo>());
            Assert.Throws<ArgumentException>(() => "linux".ParseEnum<Foo>());
        }

        [Fact]
        public void TestContainsSpaces()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensions.ContainsSpaces(null));
            Assert.True("linux\trocks".ContainsSpaces());
            Assert.True("linux\nrocks".ContainsSpaces());
            Assert.True("linux\n   \trocks".ContainsSpaces());
            Assert.False("kderocks".ContainsSpaces());
        }
    }
}
