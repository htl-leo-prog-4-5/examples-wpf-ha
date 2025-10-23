using System.Collections.Generic;

namespace GradeCalc.Core.SuccessDetermination
{
    /// <summary>
    ///     A handler for determining success based on grades
    /// </summary>
    public interface ISuccessDetermination
    {
        /// <summary>
        ///     Determines success
        /// </summary>
        /// <param name="grades">The grades</param>
        /// <param name="avgGrade">The average grade</param>
        /// <returns>The success type</returns>
        SuccessType EvaluateAverageGrade(IReadOnlyCollection<int> grades, double avgGrade);
    }
}