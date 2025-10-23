using System;
using System.Globalization;
using System.Windows.Data;
using GradeCalc.Core.SuccessDetermination;

namespace GradeCalc.Core.Converter
{
    /// <summary>
    ///     Value converter for SuccessType > string
    /// </summary>
    public sealed class SuccessToStringConverter : IValueConverter
    {
        /// <summary>
        ///     Meant to return flavor text for a <see cref="SuccessType"/> object
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SuccessType success))
            {
                throw new ArgumentException(nameof(value));
            }
            switch (success)
            {
                case SuccessType.Unknown:
                    return "Unknown";
                case SuccessType.Failed:
                    return "Failed";
                case SuccessType.Passed:
                    return "Passed";
                case SuccessType.WithSuccess:
                    return "Passed with success";
                case SuccessType.WithDistinction:
                    return "Passed with distinction";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Not supported
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // not needed
            throw new NotSupportedException();
        }
    }
}