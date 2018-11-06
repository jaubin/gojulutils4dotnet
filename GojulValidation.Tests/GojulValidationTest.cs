using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

using System.Linq;

using Moq;

using Org.Gojul.Validation;

namespace Org.Gojul.Validation.Tests
{
    public class GojulValidationTest
    {

        private class ValidatorKO : IGojulValidator<string, string, string>
        {
            public Task ValidateAsync(string element, GojulValidationErrorMessageContainer<string, string> errorMessageContainer)
            {
                errorMessageContainer.AddError(new GojulValidationErrorMessage<string, string>("foo", "bar"));
                return Task.CompletedTask;
            }
        }

        [Fact]
        public async Task TestValidateAsyncWithNullObjectThrowsException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => GojulValidation.ValidateAsync((string) null,
                new[] { BuildValidator().Object, BuildValidator().Object }.ToList()));
        }

        [Fact]
        public async Task TestValidateAsyncWithValidatorListThrowsException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => GojulValidation.ValidateAsync<string, string, string>("hello",
                null));
        }

        [Fact]
        public async Task TestValidateAsyncWithAllOK()
        {
            var validator1 = BuildValidator();
            var validator2 = BuildValidator();
            var validator3 = BuildValidator();

            
            await GojulValidation.ValidateAsync("hello", new[] { validator1.Object, validator2.Object, validator3.Object });
            

            CheckValidator("hello", validator1);
            CheckValidator("hello", validator2);
            CheckValidator("hello", validator3);
        }

        [Fact]
        public async Task TestValidateAsync()
        {
            var validator1 = BuildValidator();
            var validator2 = BuildValidator();
            var validator3 = new ValidatorKO();
            var validator4 = BuildValidator();

            try
            {
                await GojulValidation.ValidateAsync("hello", new[] { validator1.Object, validator2.Object, validator3, validator4.Object });
                throw new Xunit.Sdk.XunitException("Failed test");
            }
            catch (GojulValidationException<string, string> e)
            {
                var errorMsgContainer = e.ErrorMessageContainer;
                Assert.Equal(1, errorMsgContainer.Messages.Count);
                Assert.Equal(new GojulValidationErrorMessage<string, string>("foo", "bar"), errorMsgContainer.Messages[0]);
            }

            CheckValidator("hello", validator1);
            CheckValidator("hello", validator2);
            CheckValidator("hello", validator4);
        }

        private Mock<IGojulValidator<string, string, string>> BuildValidator()
        {
            return new Mock<IGojulValidator<string, string, string>>();
        }

        private void CheckValidator(string val,
            Mock<IGojulValidator<string, string, string>> validator)
        {
            validator.Verify(e => e.ValidateAsync(val, It.IsAny<GojulValidationErrorMessageContainer<string, string>>()));
        }
    }
}
