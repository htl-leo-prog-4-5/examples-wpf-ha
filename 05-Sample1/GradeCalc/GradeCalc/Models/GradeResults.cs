using GradeCalc.Core.SuccessDetermination;
using Prism.Mvvm;

namespace GradeCalc.Models
{
    /// <summary>
    ///     A model containing calculated results based on grades
    /// </summary>
    public sealed class GradeResults : BindableBase
    {
        private double _avgGrade;
        private SuccessType _success;

        public GradeResults()
        {
            AvgGrade = 0d;
            Success = SuccessType.Unknown;
        }

        /// <summary>
        ///     Gets the average grade label text
        /// </summary>
        public string AvgGradeDesc => "Average grade: ";

        /// <summary>
        ///     Gets the success label text
        /// </summary>
        public string SuccessDesc => "Success: ";

        /// <summary>
        ///     Gets or sets the success type
        /// </summary>
        public SuccessType Success
        {
            get => _success;
            set => SetProperty(ref _success, value);
        }

        /// <summary>
        ///     Gets or sets the average grade
        /// </summary>
        public double AvgGrade
        {
            get => _avgGrade;
            set => SetProperty(ref _avgGrade, value);
        }
    }
}