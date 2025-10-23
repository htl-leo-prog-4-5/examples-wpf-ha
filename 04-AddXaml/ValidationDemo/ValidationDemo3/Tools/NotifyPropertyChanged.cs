namespace ValidationDemo3.Tools
{
    using System.Collections.Generic;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class NotifyPropertyChanged : INotifyPropertyChanged,
        INotifyDataErrorInfo, IValidatableObject
    {
        // Validation
        private bool _hasErrors;
        private bool _isValid;
        private bool _isChanged;
        private string _dbError;

        protected readonly Dictionary<string, List<string>> Errors;

        public NotifyPropertyChanged()
        {
            Errors = new Dictionary<string, List<string>>();
        }


        //public static bool InDesignMode()
        //{
        //    return !(Application.Current is App);
        //}

        #region Validation

        /// <summary>
        /// Aufruf der Validierung aller Properties.
        /// Muss aufgerufen werden, wenn sich ein Property ändert
        /// "Alte" Fehler werden wieder gemeldet (gelöscht und angelegt)
        ///     Zuerst Errors löschen und UI verständigen
        ///     ValidationResults und ValidationContext anlegen
        ///     Über Validator ganzes Objekt validieren
        ///     Aus ValidationResults fehlerhafte Properties "distinct" ermitteln
        ///     Für jedes Property Fehlermeldungen in Errors abspeichern
        ///         und Notification für ErrorsChanged für Property auslösen
        ///     HasErrors und IsValid ==> Notification
        /// </summary>
        protected void Validate()
        {
            ClearErrors();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);
            Validator.TryValidateObject(this, validationContext, validationResults, true);
            if (validationResults.Any())
            {
                // SelectMany "flacht" die Ergebnisse einer Collection of Collections aus
                // Distinct sorgt dafür, dass jeder Propertyname nur einmal vorkommt
                // (auch wenn er mehrere Fehler auslöst)
                var propertyNames = validationResults.SelectMany(
                    validationResult => validationResult.MemberNames).Distinct().ToList();
                // alle Fehlermeldungen aller Properties in Errors-Collection speichern
                foreach (var propertyName in propertyNames)
                {
                    Errors[propertyName] = validationResults
                        .Where(validationResult => validationResult.MemberNames.Contains(propertyName))
                        .Select(r => r.ErrorMessage)
                        .Distinct() // gleiche Fehlermeldungen unterdrücken
                        .ToList();
                    // UI verständigen, dass Fehler entdeckt wurden (etwas über Ziel geschossen)
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                }
            }

            HasErrors = Errors.Any();
            IsValid = Errors.Count == 0 && string.IsNullOrEmpty(DbError);
        }

        /// <summary>
        /// Fehlerliste löschen und Properties verständigen
        /// </summary>
        protected void ClearErrors()
        {
            DbError = "";
            foreach (var propertyName in Errors.Keys.ToList())
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Fehlermeldungen für das Property zrückgeben
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns>Fehlermeldungen für das Property</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return propertyName != null && Errors.ContainsKey(propertyName)
                ? Errors[propertyName]
                : Enumerable.Empty<string>();
        }

        /// <summary>
        /// Gibt es derzeit im ViewModel Fehler
        /// Damit die Notification funktioniert, wird umständlich ein 
        /// echtes Property angelegt.
        /// </summary>
        public bool HasErrors
        {
            get { return _hasErrors; }
            set
            {
                _hasErrors = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Sind alle Properties valide, gibt es in der Errorscollection keine Einträge
        /// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }

        public bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                _isChanged = value;
                OnPropertyChanged();
            }
        }

        public String DbError
        {
            get { return _dbError; }
            set
            {
                _dbError = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Haben sich die Fehlermeldungen verändert?
        /// Verständigung des UI
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        //public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new NotImplementedException();
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}