using System;
using System.Collections.Generic;
using System.Linq;

using Conditions;

namespace Org.Gojul.Extensions
{
    /// <summary>
    /// Class <code>CollectionExtension</code> contains various extension
    /// methods for <see cref="Collection"/> instances.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Split the collection <code>collection</code> in chunks of size <code>chunkSize</code>,
        /// and return the split collection.
        /// </summary>
        /// <typeparam name="T">the type of elements of the collection.</typeparam>
        /// <param name="collection">the collection to split.</param>
        /// <param name="chunkSize">the chunk size of collection elements.</param>
        /// <returns>the split collection.</returns>
        /// <exception cref="ArgumentNullException">if <code>collection</code> is <code>null</code>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">if <code>chunkSize</code> is smaller or equal to zero.</exception>
        public static IEnumerable<IEnumerable<T>> Split<T>(this ICollection<T> collection, int chunkSize)
        {
            Condition.Requires(collection).IsNotNull();
            Condition.Requires(chunkSize).IsGreaterThan(0);
                        
            var result = new List<IEnumerable<T>>();

            int total = 0;

            while (total < collection.Count)
            {
                result.Add(collection.Skip(total).Take(chunkSize));
                total += chunkSize;
            }

            return result;
        }
    }
}
