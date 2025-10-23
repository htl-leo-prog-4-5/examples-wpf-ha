using System;
using System.IO;

using WPFApp.ViewModels;

namespace UnitTest.ViewModels;

using System.Linq;

using FluentAssertions;

using Logic.DTO;
using Logic.Helpers;

using Xunit;

public class HalfAndTimeTests
{
    [Theory]
    [InlineData("0'",     0, 0)]
    [InlineData("44'",    0, 44)]
    [InlineData("45'+0",  0, 45)]
    [InlineData("45'+1",  0, 46)]
    [InlineData("45'",    1, 0)]
    [InlineData("89'",    1, 44)]
    [InlineData("90'+0",  1, 45)]
    [InlineData("90'+1",  1, 46)]
    [InlineData("90'",    2, 0)]
    [InlineData("104'",   2, 14)]
    [InlineData("105'+0", 2, 15)]
    [InlineData("105'+1", 2, 16)]
    [InlineData("105'",   3, 0)]
    [InlineData("119'",   3, 14)]
    [InlineData("120'+0", 3, 15)]
    [InlineData("120'+1", 3, 16)]
    public void ConvertToHalfAndTimeTest(string time, int expectedHalf, int expectedTimeInHalf)
    {
        var timeConverted = time.ToHalfAndTime();

        timeConverted.half.Should().Be(expectedHalf);
        timeConverted.time.Should().Be(expectedTimeInHalf);
    }

    [Theory]
    [InlineData(0, 0,  "0'")]
    [InlineData(0, 44, "44'")]
    [InlineData(0, 45, "45'+0")]
    [InlineData(0, 46, "45'+1")]
    [InlineData(1, 0,  "45'")]
    [InlineData(1, 44, "89'")]
    [InlineData(1, 45, "90'+0")]
    [InlineData(1, 46, "90'+1")]
    [InlineData(2, 0,  "90'")]
    [InlineData(2, 14, "104'")]
    [InlineData(2, 15, "105'+0")]
    [InlineData(2, 16, "105'+1")]
    [InlineData(3, 0,  "105'")]
    [InlineData(3, 14, "119'")]
    [InlineData(3, 15, "120'+0")]
    [InlineData(3, 16, "120'+1")]
    public void ConvertFromHalfAndTimeTest(int half, int timeInHalf, string expectedTime)
    {
        var timeConverted = (half, timeInHalf).ToEventTime();

        timeConverted.Should().Be(expectedTime);
    }
}