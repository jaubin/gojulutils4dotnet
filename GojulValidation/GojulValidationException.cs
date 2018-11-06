using System;

using Conditions;

namespace Org.Gojul.Validation
{
    /// <summary>
    /// Class <code>GojulValidationException</code> is the class
    /// of exceptions thrown when business validation error are encountered.
    /// </summary>
    /// <typeparam name="T">the type of the object to validate.</typeparam>
    /// <typeparam name="K">the type of the UI identifier widgets where stuff must be identified.</typeparam>
    public class GojulValidationException<K, V> : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errorMessageContainer">the error message container which is the cause of the error.</param>
        /// <exception cref="ArgumentNullException">if <code>errorMessageContainer</code> is <code>null</code></exception>.
        public GojulValidationException(GojulValidationErrorMessageContainer<K, V> errorMessageContainer) : this("A validation error occurred", errorMessageContainer)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">the error message.</param>
        /// <param name="errorMessageContainer">the error message container which is the cause of the error.</param>
        /// <exception cref="ArgumentNullException">if <code>errorMessageContainer</code> is <code>null</code></exception>.
        public GojulValidationException(string message, GojulValidationErrorMessageContainer<K, V> errorMessageContainer) : base(message)
        {
            Condition.Requires(errorMessageContainer).IsNotNull();
            this.ErrorMessageContainer = errorMessageContainer;
        }

        public GojulValidationErrorMessageContainer<K, V> ErrorMessageContainer { get; }
    }
}
