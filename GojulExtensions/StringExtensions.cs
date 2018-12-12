using System;
using System.Collections.Generic;
using System.Text;

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
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
