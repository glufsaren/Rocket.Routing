// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumerableExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rocket.Routing.Extensions
{
    internal static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty(this IEnumerable collection)
        {
            return collection == null || !collection.Cast<object>().Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static IList<T> Enumerate<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return new List<T>();
            }

            return collection as IList<T> ?? collection.ToList();
        }
    }
}