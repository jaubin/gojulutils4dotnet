using System;
using Xunit;

namespace Org.Gojul.Validation.Tests
{
    public class GojulValidationErrorMessageContainerTest
    {
        [Fact]
        public void TestAddErrorWithNullErrorThrowsException()
        {
            var errorMsgContainer = new GojulValidationErrorMessageContainer<string, string>();
            Assert.Throws<ArgumentNullException>(() => errorMsgContainer.AddError(null));
        }

        [Fact]
        public void TestAddError()
        {
            var errorMsgContainer = new GojulValidationErrorMessageContainer<string, string>();

            var msg = new GojulValidationErrorMessage<string, string>("hello", "world");
            errorMsgContainer.AddError(msg);

            Assert.Equal(1, errorMsgContainer.Messages.Count);
            Assert.Equal(msg, errorMsgContainer.Messages[0]);
        }

        [Fact]
        public void TestAddErrorOnAssertionWithNullFuncThrowsException()
        {
            var errorMsgContainer = new GojulValidationErrorMessageContainer<string, string>();

            Assert.Throws<ArgumentNullException>(() => errorMsgContainer.AddError(true, null));
        }

        [Fact]
        public void TestAddErrorOnAssertionWithFalseAssertionAndNullGeneratedMessageThrowsException()
        {
            var errorMsgContainer = new GojulValidationErrorMessageContainer<string, string>();

            Assert.Throws<ArgumentNullException>(() => errorMsgContainer.AddError(false, () => null));
        }

        [Fact]
        public void TestAddErrorOnAssertion()
        {
            var errorMsgContainer = new GojulValidationErrorMessageContainer<string, string>();

            errorMsgContainer.AddError(1 == 1, () => new GojulValidationErrorMessage<string, string>("foo", "bar"));
            errorMsgContainer.AddError(1 == 0, () => new GojulValidationErrorMessage<string, string>("hello", "world"));

            Assert.Equal(1, errorMsgContainer.Messages.Count);
            Assert.Equal(new GojulValidationErrorMessage<string, string>("hello", "world"), errorMsgContainer.Messages[0]);
        }

        [Fact]
        public void TestHasErrors()
        {
            var errorMsgContainer = new GojulValidationErrorMessageContainer<string, string>();

            Assert.False(errorMsgContainer.HasErrors);

            errorMsgContainer.AddError(new GojulValidationErrorMessage<string, string>("hello", "world"));
            Assert.True(errorMsgContainer.HasErrors);
        }
    }
}
