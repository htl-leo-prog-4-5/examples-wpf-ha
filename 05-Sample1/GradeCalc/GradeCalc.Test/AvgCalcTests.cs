using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvgCalc.Contract;
using FluentAssertions;
using GradeCalc.Core;
using GradeCalc.Core.AvgCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace GradeCalc.Test
{
    [TestClass]
    public sealed class AvgCalcTests : TestBase
    {
        [TestMethod]
        public async Task CheckAvgCorrect()
        {
            const double EXPECTED_RESULT = 5d / 3d;
            IReadOnlyCollection<int> testSet = new List<int> {1, 3, 1};
            PrepareProviderTest();
            var provider = Dependency.Resolve<AvgCalcProvider>();

            var result = await provider.CalcAvg(testSet);

            result.Should().BeApproximately(EXPECTED_RESULT, double.Epsilon, "valid set yields average including duplicates");
        }

        [TestMethod]
        public async Task CheckResForEmptySet()
        {
            const double EXPECTED_RESULT = 0d;
            IReadOnlyCollection<int> testSet = new List<int>(0);
            PrepareProviderTest();
            var provider = Dependency.Resolve<AvgCalcProvider>();

            var result = await provider.CalcAvg(testSet);

            result.Should().BeApproximately(EXPECTED_RESULT, double.Epsilon, "empty set yield 0");
        }

        [TestMethod]
        public async Task CheckResForNullSet()
        {
            const double EXPECTED_RESULT = 0d;
            IReadOnlyCollection<int> testSet = null;
            PrepareProviderTest();
            var provider = Dependency.Resolve<AvgCalcProvider>();

            // ReSharper disable once ExpressionIsAlwaysNull - intended
            var result = await provider.CalcAvg(testSet);

            result.Should().BeApproximately(EXPECTED_RESULT, double.Epsilon, "null set yields 0");
        }

        [TestMethod]
        public void CheckNumberSetValidation()
        {
            var validator = new NumberSetValidator();

            validator.IsValidNumberSet(new[] {1, 2, 3}).Should().BeTrue("valid set");
            validator.IsValidNumberSet(new[] {-1, -2}).Should().BeTrue("negative values are fine");
            validator.IsValidNumberSet(new int[0]).Should().BeFalse("empty set not valid");
            validator.IsValidNumberSet(null).Should().BeFalse("null set not valid");
        }

        private static void PrepareProviderTest()
        {
            Dependency.RegisterInstance(GetProxyMock());
            Dependency.RegisterType<INumberSetValidator, NumberSetValidator>();
        }

        private static IAvgCalcProxy GetProxyMock()
        {
            var proxyMock = Substitute.For<IAvgCalcProxy>();
            proxyMock.RemoteCalcAvg(Arg.Any<CalculationRequest>())
                .Returns(ci =>
                {
                    var calcRes = ci.ArgAt<CalculationRequest>(0).Numbers.Average();
                    return new CalculationResponse
                    {
                        IncludeDuplicates = false,
                        Result = calcRes
                    };
                });
            return proxyMock;
        }
    }
}