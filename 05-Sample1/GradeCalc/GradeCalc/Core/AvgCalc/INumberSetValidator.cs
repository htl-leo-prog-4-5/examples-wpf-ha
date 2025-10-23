using System.Collections.Generic;

namespace GradeCalc.Core.AvgCalc
{
    /// <summary>
    ///     A helper utility which allows to validate a collection of grades
    /// </summary>
    public interface INumberSetValidator
    {
        /// <summary>
        ///     Checks provided grade collection
        /// </summary>
        /// <param name="numbers">The collection to validate</param>
        /// <returns>True if the collection contains valid grades, false otherwise</returns>
        bool IsValidNumberSet(IReadOnlyCollection<int> numbers);
    }
}