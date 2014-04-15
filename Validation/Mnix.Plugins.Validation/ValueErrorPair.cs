using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Mnix.Plugins.Validation.Rules;

namespace Mnix.Plugins.Validation
{
    public class ValueErrorPair : INotifyPropertyChanged
    {
        private IValidationRule[] mValidationRules;

        private object mValue;
        public object Value
        {
            get { return mValue; }
            set
            {
                mValue = value;
                OnPropertyChanged("Value");
            }
        }

        private string mErrorMessage;
        public string ErrorMessage
        {
            get { return mErrorMessage; }
            set { mErrorMessage = value; OnPropertyChanged("ErrorMessage"); ; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ValueErrorPair(params IValidationRule[] validationRules)
        {
            if (validationRules == null)
            {
                throw new ArgumentNullException("validationRules");
            }
            if (validationRules.Length == 0)
            {
                string paramName = "validationRules";
                throw new ArgumentException(string.Format("{0}.Length == 0, must be greater than 0", paramName), paramName);
            }
            mValidationRules = validationRules;
        }

        public void Validate()
        {
            foreach (IValidationRule validationRule in mValidationRules)
            {
                if (!validationRule.isValid(Value))
                {
                    ErrorMessage = validationRule.ErrorMessage;
                    return;
                }
            }
            // Passed all validations
            ErrorMessage = null;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("'propertyName' (" + propertyName + ") must be a valid identifier");
            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }

    public class ValueErrorPair<TProperty> : ValueErrorPair
    {
        public new TProperty Value
        {
            get { return (TProperty)base.Value; }
            set { base.Value = value; }
        }

        public ValueErrorPair(params IValidationRule<TProperty>[] validationRules) : base(validationRules) { }
        public ValueErrorPair(TProperty initialValue, params IValidationRule<TProperty>[] validationRules) : base(validationRules)
        {
            Value = initialValue;
        }

    }
}
