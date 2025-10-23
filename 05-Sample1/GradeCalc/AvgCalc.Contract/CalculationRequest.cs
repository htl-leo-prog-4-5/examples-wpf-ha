using System.Collections.Generic;

namespace AvgCalc.Contract
{
    public sealed class CalculationRequest : CalculationRequestBase
    {
        public IList<int> Numbers { get; set; }
    }
}