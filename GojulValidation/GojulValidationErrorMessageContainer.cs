using Conditions;
using System;
using System.Collections.Generic;

namespace Org.Gojul.Validation
{
    /// <summary>
    /// Class <code>GojulValidationErrorMessageContainer</code> contains a list
    /// of <see cref="GojulValidationErrorMessage{TK, TV}"/> instances. This way, it
    /// makes it easier to collect all the errors encountered.
    /// </summary>
    /// <typeparam name="TK">the type of UI identifiers.</typeparam>
    /// <typeparam name="TV">the type of messages.</typeparam>
    public class GojulValidationErrorMessageContainer<TK, TV>
    {
        private readonly List<GojulValidationErrorMessage<TK, TV>> _messages;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GojulValidationErrorMessageContainer()
        {
            this._messages = new List<GojulValidationErrorMessage<TK, TV>>();
        }

        /// <summary>
        /// Return the list of messages.
        /// </summary>
        public IList<GojulValidationErrorMessage<TK, TV>> Messages
        {
            get
            {
                // We need this as the container may be used in an async/await context.
                lock (this._messages)
                {
                    return _messages.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Indicate whether this container contains errors or not.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return Messages.Count > 0;
            }
        }

        /// <summary>
        /// Add an error message to this container.
        /// </summary>
        /// <param name="msg">the error message to add.</param>
        /// <exception cref="ArgumentNullException">if <code>msg</code> is <code>null</code>.</exception>
        public void AddError(GojulValidationErrorMessage<TK, TV> msg)
        {
            Condition.Requires(msg).IsNotNull();

            // We need this as the container may be used in an async/await context.
            lock (this._messages)
            {
                this._messages.Add(msg);
            }
        }

        /// <summary>
        /// If <code>assertion</code> is <code>false</code>, add the error message designated by
        /// the <code>msg</code> function to this error list, otherwise does nothing.
        /// </summary>
        /// <param name="assertion">the assertion to test.</param>
        /// <param name="msg">the message generator used to generate the error message.</param>
        /// <exception cref="ArgumentNullException">if <code>msg</code> is <code>null</code>,
        /// or if it generates a <code>null</code> error message.</exception>
        public void AddError(bool assertion, Func<GojulValidationErrorMessage<TK, TV>> msg)
        {
            Condition.Requires(msg).IsNotNull();

            if (!assertion)
            {
                var errorMsg = msg();
                AddError(errorMsg);
            }
        }

        public override string ToString()
        {
            return this._messages.ToString();
        }
    }
}
