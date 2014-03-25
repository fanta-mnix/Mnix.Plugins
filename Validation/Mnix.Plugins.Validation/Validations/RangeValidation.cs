using System;

namespace Mnix.Plugins.Validation.Rules
{
    public class RangeValidation : IValidationRule<string>
    {
        public int MinValue { get; protected set; }
        public int MaxValue { get; protected set; }

        private readonly string mErrorMessage;
        public string ErrorMessage
        {
            get { return mErrorMessage; }
        }

        public RangeValidation(int minValue, int maxValue, string errorMessage = null)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            mErrorMessage = string.Format(this.GetErrorText(errorMessage), MinValue, MaxValue);
        }

        public bool isValid(string value)
        {
            int numValue;
            if (!int.TryParse(value, out numValue))
            { // Not a number
                return false;
            }
            return numValue <= MaxValue && numValue >= MinValue;
        }

        bool IValidationRule.isValid(object value)
        {
            return isValid((string)value);
        }
        
		public string TextNamespace { get; set; }
		public string TextResourceFile { get; set; }
    }
}

