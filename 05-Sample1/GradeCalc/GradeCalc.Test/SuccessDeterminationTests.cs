using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAssertions;
using GradeCalc.Core.SuccessDetermination;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GradeCalc.Test
{
    [TestClass]
    public sealed class SuccessDeterminationTests : TestBase
    {
        [TestMethod]
        public void CheckPassed()
        {
            CalcSuccess(new[] {2, 3, 2}, 2.33d).Should().Be(SuccessType.Passed);
            CalcSuccess(new[] {1, 1, 3}, 1.67d).Should().Be(SuccessType.WithSuccess);
            CalcSuccess(new[] {1, 5, 3}, 3d).Should().NotBe(SuccessType.Passed).And.Be(SuccessType.Failed);
        }

        [TestMethod]
        public void CheckWithSuccess()
        {
            CalcSuccess(new[] {2, 1, 2}, 1.67d).Should().Be(SuccessType.WithSuccess);
        }

        [TestMethod]
        public void CheckWithDistinction()
        {
            CalcSuccess(new[] {1, 1, 2}, 1.33d).Should().Be(SuccessType.WithDistinction);
        }

        [TestMethod]
        public void CheckFailed()
        {
            CalcSuccess(new[] {5, 4, 2}, 3.67d).Should().Be(SuccessType.Failed);
            CalcSuccess(new[] {5, 5, 5}, 5d).Should().Be(SuccessType.Failed);
            CalcSuccess(new[] {4, 4, 4}, 4d).Should().NotBe(SuccessType.Failed).And.Be(SuccessType.Passed);
        }

        [TestMethod]
        public void CheckError()
        {
            Action emptyColl = () => CalcSuccess(new int[0], 3);
            Action invalidAvg1 = () => CalcSuccess(new[] {1, 2, 3}, -2d);
            Action invalidAvg2 = () => CalcSuccess(new[] {1, 2, 3}, 0.2d);

            emptyColl.ShouldThrow<ArgumentException>();
            invalidAvg1.ShouldThrow<ArgumentException>();
            invalidAvg2.ShouldThrow<ArgumentException>();
        }

        private static SuccessType CalcSuccess(IList<int> grades, double averageGrade)
        {
            var helper = new SuccessDetermination();
            return helper.EvaluateAverageGrade(new ReadOnlyCollection<int>(grades), averageGrade);
        }
    }
}