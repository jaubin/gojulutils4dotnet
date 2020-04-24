using Conditions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Org.Gojul.Validation
{
    /// <summary>
    /// Class <code>GojulValidation</code> contains various utility methods
    /// for validation of elements.
    /// </summary>
    public static class GojulValidation
    {
        /// <summary>
        /// Run the validation rules contained in <code>validators</code>
        /// for element <code>element</code>. Throw an exception if validation
        /// fails.
        /// </summary>
        /// <typeparam name="T">the type of the element to validate.</typeparam>
        /// <typeparam name="TK">the type of the UI element keys on which errors must be displayed.</typeparam>
        /// <typeparam name="TV">the type of the error messages.</typeparam>
        /// <param name="element">the element to validate.</param>
        /// <param name="validators">the validators.</param>
        /// <exception cref="ArgumentNullException">if any of the method parameters is <code>null</code>.</exception>
        /// <exception cref="GojulValidationException{TK, TV}">if the validation fails.</exception>
        public static async Task ValidateAsync<T, TK, TV>(T element, IEnumerable<IGojulValidator<T, TK, TV>> validators)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element is null");
            }

            Condition.Requires(validators).IsNotNull();

            var errorMessageContainer = new GojulValidationErrorMessageContainer<TK, TV>();

            var tasks = new List<Task>();
            foreach (var v in validators)
            {
                tasks.Add(v.ValidateAsync(element, errorMessageContainer));
            }
            await Task.WhenAll(tasks).ConfigureAwait(false);

            if (errorMessageContainer.HasErrors)
            {
                throw new GojulValidationException<TK, TV>("Validation failed", errorMessageContainer);
            }
        }
    }
}
