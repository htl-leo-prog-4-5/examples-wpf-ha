using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvgCalc.Contract;
using JetBrains.Annotations;

namespace GradeCalc.Core.AvgCalc
{
    /// <inheritdoc />
    [UsedImplicitly]
    internal sealed class AvgCalcProvider : IAvgCalcProvider
    {
        private readonly IAvgCalcProxy _proxy;
        [NotNull] private readonly INumberSetValidator _validator;

        /// <summary>
        ///     default ctor
        /// </summary>
        public AvgCalcProvider([NotNull] IAvgCalcProxy proxy, [NotNull] INumberSetValidator validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        /// <inheritdoc />
        public async Task<double> CalcAvg(IReadOnlyCollection<int> numbers)
        {
            if (!_validator.IsValidNumberSet(numbers))
            {
                return 0d;
            }
            var response = await _proxy.RemoteCalcAvg(new CalculationRequest
            {
                IncludeDuplicates = true,
                Numbers = numbers.ToList()
            });
            return response.Result;
        }
    }
}