using System;

using Conditions;

namespace Org.Gojul.Validation
{
    /// <summary>
    /// Class <code>GojulValidationException</code> is the class
    /// of exceptions thrown when business validation error are encountered.
    /// </summary>
    /// <typeparam name="T">the type of the object to validate.</typeparam>
    /// <typeparam name="TK">the type of the UI identifier widgets where stuff must be identified.</typeparam>
    public class GojulValidationException<TK, TV> : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GojulValidationException()
        {
        }
  
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">the error message.</param>
        public GojulValidationException(string message): base(message)
        {
        }
   
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">the error message.</param>
        /// <param name="innerException">the root exception.</param>
        public GojulValidationException(string message, Exception innerException): base(message, innerException)
        {
        }
    


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errorMessageContainer">the error message container which is the cause of the error.</param>
        /// <exception cref="ArgumentNullException">if <code>errorMessageContainer</code> is <code>null</code></exception>.
        public GojulValidationException(GojulValidationErrorMessageContainer<TK, TV> errorMessageContainer) : this("A validation error occurred", errorMessageContainer)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">the error message.</param>
        /// <param name="errorMessageContainer">the error message container which is the cause of the error.</param>
        /// <exception cref="ArgumentNullException">if <code>errorMessageContainer</code> is <code>null</code></exception>.
        public GojulValidationException(string message, GojulValidationErrorMessageContainer<TK, TV> errorMessageContainer) : base(message)
        {
            Condition.Requires(errorMessageContainer).IsNotNull();
            this.ErrorMessageContainer = errorMessageContainer;
        }

        public GojulValidationErrorMessageContainer<TK, TV> ErrorMessageContainer { get; }
    }
}
