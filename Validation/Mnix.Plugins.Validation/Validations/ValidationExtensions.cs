using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Localization;

namespace Mnix.Plugins.Validation.Rules
{
    public static class ValidationExtensions
    {
        public static string GetErrorText(this IValidationRule rule, string entryKey = null)
        {
            if (rule == null)
            {
                throw new ArgumentNullException("rule");
            }
            if (string.IsNullOrEmpty(entryKey))
            { // Default is type name
                entryKey = rule.GetType().Name;
            }
			return Mvx.Resolve<IMvxTextProvider>().GetText(rule.TextNamespace, rule.TextResourceFile, entryKey);
        }
    }
}
