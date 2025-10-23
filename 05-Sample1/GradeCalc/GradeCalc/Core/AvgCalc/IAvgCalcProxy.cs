using System.Threading.Tasks;
using AvgCalc.Contract;

namespace GradeCalc.Core.AvgCalc
{
    /// <summary>
    ///     A proxy class for accessing the AvgCalc service
    /// </summary>
    public interface IAvgCalcProxy
    {
        /// <summary>
        ///     Calls the AvgCalc service to perform an average calculation
        /// </summary>
        /// <param name="request">The calculation request</param>
        /// <returns>The calculated value</returns>
        Task<CalculationResponse> RemoteCalcAvg(CalculationRequest request);
    }
}