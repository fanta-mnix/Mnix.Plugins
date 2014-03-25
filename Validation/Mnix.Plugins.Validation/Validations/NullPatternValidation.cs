using System;

namespace Mnix.Plugins.Validation.Rules
{
	public class NullPatternValidation : IValidationRule<string>
	{
		public static readonly NullPatternValidation Instance = new NullPatternValidation();
	
		#region IValidationRule implementation
		
		public bool isValid(string value)
		{
			return true;
		}
		
		#endregion
		
		#region IValidationRule implementation
		
		public bool isValid(object value)
		{
			return true;
		}
		
		public string ErrorMessage
		{
			get
			{
				return string.Empty;
			}
		}
        
		public string TextNamespace { get; set; }
		public string TextResourceFile { get; set; }
		
		#endregion
	}
}

