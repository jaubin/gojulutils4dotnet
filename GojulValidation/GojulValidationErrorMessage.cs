using System;
using System.Collections.Generic;

namespace Org.Gojul.Validation
{
    /// <summary>
    /// Class <code>GojulValidationErrorMessage</code> represents a simple error message to display
    /// to the user. Each message may be bound to a UI target, with a given message.
    /// </summary>
    /// <typeparam name="TK">the type of the UI target component.</typeparam>
    /// <typeparam name="TV">the type of the message to display.</typeparam>
    public sealed class GojulValidationErrorMessage<TK, TV>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="uiTarget">the UI target component identifier.</param>
        /// <param name="message">the message to display.</param>
        /// <exception cref="ArgumentNullException">if any of the method parameters is <code>null</code>.</exception>
        public GojulValidationErrorMessage(TK uiTarget, TV message)
        {
            this.UiTarget = uiTarget == null ? throw new ArgumentNullException("uiTarget is null") : uiTarget;
            this.Message = message == null ? throw new ArgumentNullException("messagae is null") : message;
        }

        /// <summary>
        /// The UI target component key.
        /// </summary>
        public TK UiTarget { get; private set; }

        /// <summary>
        /// The message key.
        /// </summary>
        public TV Message { get; private set; }

        public override bool Equals(object obj)
        {
            var message = obj as GojulValidationErrorMessage<TK, TV>;
            return message != null &&
                   EqualityComparer<TK>.Default.Equals(UiTarget, message.UiTarget) &&
                   EqualityComparer<TV>.Default.Equals(Message, message.Message);
        }

        public override int GetHashCode()
        {
            var hashCode = 1317358674;
            hashCode = hashCode * -1521134295 + EqualityComparer<TK>.Default.GetHashCode(UiTarget);
            hashCode = hashCode * -1521134295 + EqualityComparer<TV>.Default.GetHashCode(Message);
            return hashCode;
        }

        public override string ToString()
        {
            return "[ UiTarget: " + UiTarget + " - Message: " + Message + " ]";
        }
    }
}
