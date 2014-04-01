using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Localization;

namespace Mnix.Plugins.Validation.Rules
{
    public class NonEmptyValidation : IValidationRule<string>
    {
        public static readonly NonEmptyValidation Instance = new NonEmptyValidation();

        private readonly string mErrorMessage;
        public string ErrorMessage
        {
            get { return mErrorMessage; }
        }

        protected NonEmptyValidation(string errorMessage = null)
        {
            mErrorMessage = this.GetErrorText(errorMessage);
        }

        public bool isValid(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        bool IValidationRule.isValid(object value)
        {
            return isValid((string)value);
        }
    }
}
