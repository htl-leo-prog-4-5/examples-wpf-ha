namespace ValidationDemo3.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using ValidationDemo3.Models;
    using ValidationDemo3.Tools;

    public class MainWindowViewModel : BaseViewModel
    {
        #region validation

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

        #endregion

        #region Properties

        public MyData SomeData { get; } = new MyData() { StrValue1 = "Hallo", IntValue1 = 4711 };

        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Operations

        #endregion

        #region Commands

        #endregion
    }
}