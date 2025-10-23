namespace ValidationDemo3.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationDemo3.Tools;

    public class MyData : NotifyPropertyChanged
    {
        private string _strValue1;
        private int    _intValue1;
        private int    _intValue2;
        private int    _intValue3;

        [Required(ErrorMessage = "The Value is required") ]
        public string StrValue1
        {
            get => _strValue1;
            set
            {
                _strValue1 = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public int IntValue1
        {
            get => _intValue1;
            set
            {
                _intValue1 = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public int IntValue2
        {
            get => _intValue2;
            set
            {
                _intValue2 = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public int IntValue3
        {
            get => _intValue3;
            set
            {
                _intValue3 = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(StrValue1))
            {
                yield return new ValidationResult("StringValue 1 is required", new[] { nameof(StrValue1) });
            }
        }
    }
}