using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradeCalc.Core.AvgCalc
{
    /// <summary>
    ///     A helper utility for calculating the average of a range of numbers
    /// </summary>
    public interface IAvgCalcProvider
    {
        /// <summary>
        ///     Calculates the average of a range of numbers
        /// </summary>
        /// <param name="numbers">The numbers to consider. Duplicates are not removed.</param>
        /// <returns>The calculated average</returns>
        Task<double> CalcAvg(IReadOnlyCollection<int> numbers);
    }
}