using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DerbyManagement.Model
{
    public class ModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        #region " INotifyPropertyChanged "
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region " IDataErrorInfo "
        public string this[string propertyName]
        {
            get
            {
                return OnValidate(propertyName);
            }
        }

        /// <summary>
        /// Validates current instance properties using Data Annotations.
        /// </summary>
        /// <param name=“propertyName”>This instance property to validate.</param>
        /// <returns>Relevant error string on validation failure or <see cref=“System.String.Empty”/> on validation success.</returns>
        private string OnValidate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property may not be null or empty", propertyName);

            string error = string.Empty;

            var value = this.GetType().GetProperty(propertyName).GetValue(this, null);
            var results = new List<ValidationResult>();

            var context = new ValidationContext(this, null, null) { MemberName = propertyName };

            var result = Validator.TryValidateProperty(value, context, results);

            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }
            return error;
        }

        public string Error
        {
            get
            {
                throw new NotSupportedException("IDataErrorInfo.Error is not supported, use IDataErrorInfo.this[propertyName] instead.");
            }
        }
        #endregion

    }
}
