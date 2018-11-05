﻿using System;
using System.Collections.Generic;

using Conditions;

namespace Org.Gojul.Validation
{
    /// <summary>
    /// Class <code>GojulValidationErrorMessageContainer</code> contains a list
    /// of <see cref="GojulValidationErrorMessage{K, V}"/> instances. This way, it
    /// makes it easier to collect all the errors encountered.
    /// </summary>
    /// <typeparam name="K">the type of UI identifiers.</typeparam>
    /// <typeparam name="V">the type of messages.</typeparam>
    public class GojulValidationErrorMessageContainer<K, V>
    {
        private List<GojulValidationErrorMessage<K, V>> messages;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GojulValidationErrorMessageContainer()
        {
            this.messages = new List<GojulValidationErrorMessage<K, V>>();
        }

        /// <summary>
        /// Return the list of messages.
        /// </summary>
        public IList<GojulValidationErrorMessage<K, V>> Messages
        {
            get
            {
                // We need this as the container may be used in an async/await context.
                lock (this.messages)
                {
                    return messages.AsReadOnly();
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
        public void AddError(GojulValidationErrorMessage<K, V> msg)
        {
            Condition.Requires(msg).IsNotNull();

            // We need this as the container may be used in an async/await context.
            lock (this.messages)
            {
                this.messages.Add(msg);
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
        public void AddError(bool assertion, Func<GojulValidationErrorMessage<K, V>> msg)
        {
            Condition.Requires(msg).IsNotNull();

            if (!assertion)
            {
                var errorMsg = msg();
                AddError(errorMsg);
            }
        }

        public override bool Equals(object obj)
        {
            var container = obj as GojulValidationErrorMessageContainer<K, V>;
            return container != null &&
                   EqualityComparer<IList<GojulValidationErrorMessage<K, V>>>.Default.Equals(messages, container.messages);
        }

        public override int GetHashCode()
        {
            return -99712259 + EqualityComparer<IList<GojulValidationErrorMessage<K, V>>>.Default.GetHashCode(messages);
        }

        public override string ToString()
        {
            return this.messages.ToString();
        }
    }
}
