using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mnix.Plugins.Validation.Rules
{
    public interface IValidationRule
    {
        string ErrorMessage { get; }
        bool isValid(object value);
		string TextNamespace { get; set; }
		string TextResourceFile { get; set; }
    }

    public interface IValidationRule<T> : IValidationRule
    {
        bool isValid(T value);
    }
}
