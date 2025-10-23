using System;
using System.Collections.Generic;
using System.Linq;

namespace AvgCalc.Service.Core
{
    internal sealed class CalculationManager
    {
        public double CalculateAverage(IReadOnlyCollection<int> numbers, bool includeDuplicates)
        {
            if (numbers == null || numbers.Count == 0)
            {
                throw new ArgumentException(nameof(numbers));
            }

            IEnumerable<int> collection = includeDuplicates ? numbers : numbers.Distinct();
            return collection.Average();
        }
    }
}