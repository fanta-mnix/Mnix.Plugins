using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mnix.Plugins.Validation.Rules
{
    public class ValidationRuleWrapper<T> : IValidationRule<T>
    {
        private Predicate<T> mPredicate;
        private string mErrorMessage;

        public string ErrorMessage
        {
            get { return mErrorMessage; }
        }

        public ValidationRuleWrapper(Predicate<T> predicate, string errorMessage)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            mPredicate = predicate;
            mErrorMessage = this.GetErrorText(errorMessage);
        }

        public bool isValid(T value)
        {
            return mPredicate(value);
        }

        bool IValidationRule.isValid(object value)
        {
            return isValid((T)value);
        }
        
		public string TextNamespace { get; set; }
		public string TextResourceFile { get; set; }
    }
}
