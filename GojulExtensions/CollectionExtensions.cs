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

        /// <summary>
        /// Return <code>true</code> if <code>list</code> and <code>other</code>
        /// have the same size, and their elements appear in the same order, <code>false</code>
        /// otherwise. In other words this method performs logical comparison between lists,
        /// what is naturally available in Java... Note however that this method won't work
        /// for list of collections...
        /// </summary>
        /// <typeparam name="T">the type of elements in the list.</typeparam>
        /// <param name="list">the first list to compare.</param>
        /// <param name="other">the second list to compare.</param>
        /// <returns><code>true</code> if <code>list</code> and <code>other</code>
        /// have the same size, and their elements appear in the same order, <code>false</code>
        /// otherwise.</returns>
        public static bool ListEquals<T>(this IList<T> list, IList<T> other)
        {
            if (list is null) return other is null;
            if (other is null) return false;

            var count1 = list.Count;
            var count2 = other.Count;
            if (count1 != count2) return false;

            return list.SequenceEqual(other);
        }
    }
}
