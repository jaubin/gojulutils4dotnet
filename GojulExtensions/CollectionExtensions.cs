using Conditions;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// for list of collections since C# does not implement Equals/HashCode for collections properly...
        /// </summary>
        /// <typeparam name="T">the type of elements in the list. It must implement properly the Equals method.</typeparam>
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

        /// <summary>
        /// Return <code>true</code> if <code>dictionary</code> and <code>other</code>
        /// are logically equal, i.e. they have the same size and the same key/value pairs,
        /// <code>false</code> otherwise. Note that this method will probably not work for dictionary
        /// of collections since C# does not implement Equals/HashCode for collections properly...
        /// </summary>
        /// <typeparam name="TK">the key type. It must implement properly the Equals method.</typeparam>
        /// <typeparam name="TV">the value type. It must implement properly the Equals method.</typeparam>
        /// <param name="dictionary">the first dictionary.</param>
        /// <param name="other">the second dictionary.</param>
        /// <returns><code>true</code> if <code>dictionary</code> and <code>other</code>
        /// are logically equal, i.e. they have the same size and the same key/value pairs,
        /// <code>false</code> otherwise.</returns>
        public static bool DictEquals<TK, TV>(this IDictionary<TK, TV> dictionary, IDictionary<TK, TV> other)
        {
            if (dictionary is null) return other is null;
            if (other is null) return false;

            if (dictionary.Count != other.Count)
            {
                return false;
            }

            // Courtesy of https://stackoverflow.com/questions/3804367/testing-for-equality-between-dictionaries-in-c-sharp
            return !dictionary.Except(other).Any();
        }
    }
}
