using System.Collections.Generic;

namespace GradeCalc.Core.Extensions
{
    internal static class CollectionExtensions
    {
        /// <summary>
        ///     Determines if a collection is null or empty
        /// </summary>
        public static bool IsNullOrEmpty<TItem>(this IReadOnlyCollection<TItem> self) => !(self?.Count > 0);
    }
}