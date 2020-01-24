using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Conditions;

namespace Org.Gojul.Extensions
{
    /// <summary>
    /// Class <code>StringExtensions</code> contains
    /// various useful methods to be used with strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convert the string <code>value</code> into an enum type. Note
        /// that this method is not case sensitive.
        /// </summary>
        /// <typeparam name="T">the type of the enum to parse</typeparam>
        /// <param name="value">the value to parse</param>
        /// <returns>the enum value corresponding to <code>value</code>.</returns>
        /// <exception cref="ArgumentNullException">if <code>value</code> is <code>null</code>.</exception>
        /// <exception cref="ArgumentException">if <code>value</code> does not match a value of the specified enum.</exception>
        public static T ParseEnum<T>(this string value) where T : struct
        {
            // This method is inspired by :
            // https://stackoverflow.com/questions/32542356/how-to-save-enum-in-database-as-string
            Condition.Requires(value).IsNotNull();
            
            if (!Enum.TryParse<T>(value, true, out T result) || !Enum.IsDefined(typeof(T), result))
            {
                throw new ArgumentException(string.Format("Unrecognized value {0} for type {1}", value, typeof(T)));
            }

            return result;
        }

        /// <summary>
        /// Return <code>true</code> if <code>value</code> contains spaces, <code>false</code>
        /// otherwise. This covers all kind of spaces. Beware that this method can be quite slow
        /// as it has to rely on regexps.
        /// </summary>
        /// <param name="value">the value to test.</param>
        /// <returns><code>true</code> if <code>value</code> contains spaces, <code>false</code>
        /// otherwise.</returns>
        /// <exception cref="ArgumentNullException">if <code>value</code> is <code>null</code>.</exception>
        public static bool ContainsSpaces(this string value)
        {
            Condition.Requires(value).IsNotNull();
            return Regex.IsMatch(value, @"\s");
        }
    }
}
