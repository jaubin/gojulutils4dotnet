using System;
using Xunit;

using Org.Gojul.Validation;

namespace Org.Gojul.Validation.Tests
{
    public class GojulValidationErrorMessageTest
    {
        [Fact]
        public void TestConstructorWithNullUiTargetThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new GojulValidationErrorMessage<string, string>(null, "world"));
        }

        [Fact]
        public void TestConstructorWithNullMessageThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new GojulValidationErrorMessage<string, string>("hello", null));
        }

        [Fact]
        public void TestAll()
        {
            var msg = new GojulValidationErrorMessage<string, string>("hello", "world");

            Assert.Equal("hello", msg.UiTarget);
            Assert.Equal("world", msg.Message);
        }
    }
}
