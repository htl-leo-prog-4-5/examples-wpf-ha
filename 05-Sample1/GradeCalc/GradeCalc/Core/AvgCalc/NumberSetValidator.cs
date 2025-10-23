using System.Collections.Generic;
using GradeCalc.Core.Extensions;
using JetBrains.Annotations;

namespace GradeCalc.Core.AvgCalc
{
    /// <inheritdoc/>
    [UsedImplicitly]
    internal sealed class NumberSetValidator : INumberSetValidator
    {
        /// <inheritdoc/>
        public bool IsValidNumberSet(IReadOnlyCollection<int> numbers)
        {
            return !numbers.IsNullOrEmpty();
        }
    }
}