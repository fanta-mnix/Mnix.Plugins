using System;
using System.Text.RegularExpressions;

namespace Mnix.Plugins.Validation.Rules
{
    public class RegexValidation : IValidationRule<string>
    {
        private Regex mValidationRegex;
		private string mPattern;

        public string Pattern
		{
			get { return mPattern; }
			protected set
			{
				mValidationRegex = new Regex(value);
				mPattern = value;
			}
		}

        private readonly string mErrorMessage;
        public string ErrorMessage
        {
            get { return mErrorMessage; }
        }

        public RegexValidation(string pattern, string errorMessage)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException("pattern");
            }
            Pattern = pattern;
            mErrorMessage = this.GetErrorText(errorMessage);
        }

        public bool isValid(string value)
        {
            return mValidationRegex.IsMatch(value ?? string.Empty);
        }

        bool IValidationRule.isValid(object value)
        {
            return isValid((string)value);
        }
    }
}

