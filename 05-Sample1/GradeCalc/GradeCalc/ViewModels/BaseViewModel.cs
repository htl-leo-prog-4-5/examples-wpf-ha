using System;
using System.Windows.Threading;
using Prism.Mvvm;

namespace GradeCalc.ViewModels
{
    /// <summary>
    ///  Base class for all view models
    /// </summary>
    public abstract class BaseViewModel : BindableBase
    {
        protected void RunOnUIThread(Action actionToRun) => Dispatcher.CurrentDispatcher.Invoke(actionToRun);
    }
}