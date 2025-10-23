using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalc.Core.Extensions;
using JetBrains.Annotations;

namespace GradeCalc.Core.SuccessDetermination
{
    /// <inheritdoc />
    [UsedImplicitly]
    internal sealed class SuccessDetermination : ISuccessDetermination
    {
        /// <inheritdoc />
        public SuccessType EvaluateAverageGrade(IReadOnlyCollection<int> grades, double avgGrade)
        {
            if (grades.IsNullOrEmpty())
            {
                throw new ArgumentException(nameof(grades));
            }
            if (avgGrade < 1d)
            {
                throw new ArgumentException(nameof(avgGrade));
            }

            if (grades.Any(n => n >= 5))
            {
                return SuccessType.Failed;
            }
            var distinctionPossible = grades.All(n => n < 4);
            if (!distinctionPossible)
            {
                return SuccessType.Passed;
            }
            if (avgGrade <= 1.5d)
            {
                return SuccessType.WithDistinction;
            }
            return avgGrade <= 2.0d ? SuccessType.WithSuccess : SuccessType.Passed;
        }
    }
}