namespace ValidationDemo1
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    public class GradeValidationRule : ValidationRule
    {
        public int Min { get; set; } = 1;
        public int Max { get; set; } = 5;

        public GradeValidationRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int grade = 0;

            try
            {
                if (((string)value).Length > 0)
                    grade = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((grade < Min) || (grade > Max))
            {
                return new ValidationResult(false,
                    $"Please enter a grade in the range: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
