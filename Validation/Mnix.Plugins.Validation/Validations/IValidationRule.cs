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
    }

    public interface IValidationRule<T> : IValidationRule
    {
        bool isValid(T value);
    }
}
