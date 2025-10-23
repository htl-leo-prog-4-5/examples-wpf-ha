using System;
using System.Windows.Input;
using GradeCalc.Core.AvgCalc;
using GradeCalc.Core.Events;
using GradeCalc.Core.SuccessDetermination;
using GradeCalc.Models;
using JetBrains.Annotations;
using Prism.Commands;

namespace GradeCalc.ViewModels
{
    /// <summary>
    ///     Main view model
    /// </summary>
    [UsedImplicitly]
    public sealed class MainWindowViewModel : BaseViewModel
    {
        private readonly IAvgCalcProvider _avgCalcProvider;
        private readonly ICommand _calcCommand;
        private readonly GradeResults _results;
        private readonly ISuccessDetermination _successDetermination;
        private bool _calcInProgress;
        private int _enGrade;
        private int _gerGrade;
        private int _mathsGrade;

        /// <summary>
        ///     Default ctor
        /// </summary>
        public MainWindowViewModel([NotNull] IAvgCalcProvider avgCalcProvider,
            [NotNull] ISuccessDetermination successDetermination)
        {
            _successDetermination =
                successDetermination ?? throw new ArgumentNullException(nameof(successDetermination));
            _avgCalcProvider = avgCalcProvider ?? throw new ArgumentNullException(nameof(avgCalcProvider));
            MathsGrade = GerGrade = EnGrade = 5;
            _calcCommand = new DelegateCommand(PerformCalc);
            _results = new GradeResults();
            _calcInProgress = false;
        }

        /// <summary>
        ///     Gets application description text
        /// </summary>
        public string DescriptionText => "Allows to calculate stuff related to grades";

        /// <summary>
        ///     Gets maths grade label text
        /// </summary>
        public string MathsGradeText => "Maths Grade: ";

        /// <summary>
        ///     Gets ger grade label text
        /// </summary>
        public string GerGradeText => "German Grade: ";

        /// <summary>
        ///     Gets en grade label text
        /// </summary>
        public string EnGradeText => "English Grade: ";

        /// <summary>
        ///     Gets or sets maths grade
        /// </summary>
        public int MathsGrade
        {
            get => _mathsGrade;
            set => SetGradeValue(ref _mathsGrade, value, nameof(MathsGrade));
        }

        /// <summary>
        ///     Gets or sets ger grade
        /// </summary>
        public int GerGrade
        {
            get => _gerGrade;
            set => SetGradeValue(ref _gerGrade, value, nameof(GerGrade));
        }

        /// <summary>
        ///     Gets or sets en grade
        /// </summary>
        public int EnGrade
        {
            get => _enGrade;
            set => SetGradeValue(ref _enGrade, value, nameof(EnGrade));
        }

        /// <summary>
        ///     Gets or sets calculation button enabled state
        /// </summary>
        public bool CalcBtnEnabled
        {
            get => !_calcInProgress;
            set => SetProperty(ref _calcInProgress, !value);
        }

        /// <summary>
        ///     Gets results bindable object
        /// </summary>
        public GradeResults Results => _results;

        /// <summary>
        ///     Gets calculation command
        /// </summary>
        public ICommand CalculateCommand => _calcCommand;

        /// <summary>
        ///     Raised when an error occurs
        /// </summary>
        public event EventHandler<ErrorEventArgs> OnError = (_, __) => { };

        private void SetGradeValue(ref int field, int value, string propertyName)
        {
            var inValidRange = value >= 1 && value <= 5;
            if (!inValidRange)
            {
                // reset to old value
                RaisePropertyChanged(propertyName);
                return;
            }
            SetProperty(ref field, value, propertyName);
        }

        private async void PerformCalc()
        {
            try
            {
                CalcBtnEnabled = false;
                var grades = new[] {_mathsGrade, _gerGrade, _enGrade};
                var avgGrade = await _avgCalcProvider.CalcAvg(grades);
                Results.AvgGrade = avgGrade;
                Results.Success = _successDetermination.EvaluateAverageGrade(grades, avgGrade);
            }
            catch (Exception ex)
            {
                // async void method has to catch and handle all Exceptions!
                RunOnUIThread(() => { OnError(this, new ErrorEventArgs(ex.Message)); });
            }
            finally
            {
                CalcBtnEnabled = true;
            }
        }
    }
}