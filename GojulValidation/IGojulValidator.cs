using System.Threading.Tasks;

namespace Org.Gojul.Validation
{

    /// <summary>
    /// Interface <code>IGojulValidator</code> is used to perform
    /// unitary validations of business rules on input objects.
    /// </summary>
    /// <typeparam name="T">the type of the object to validate.</typeparam>
    /// <typeparam name="TK">the type of the UI identifier widgets where stuff must be identified.</typeparam>
    /// <typeparam name="TV">the type of the error messages produced.</typeparam>
    public interface IGojulValidator<T, TK, TV>
    {
        /// <summary>
        /// Validate the element <code>element</code> against the rule contained in this validator. If 
        /// the rule is not satisfied, log an error in <code>errorMessageContainer</code>.
        /// </summary>
        /// <param name="element">the element to validate.</param>
        /// <param name="errorMessageContainer">the error message container used.</param>
        Task ValidateAsync(T element, GojulValidationErrorMessageContainer<TK, TV> errorMessageContainer);
    }

    
}
