using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Localization;

namespace Mnix.Plugins.Validation
{
	public abstract class EnumTitlePair
	{	
		public string Title
		{
			get;
			protected set;
		}

		public override string ToString()
		{
			return Title;
		}

		public virtual object Value
		{
			get;
			protected set;
		}
		
		public abstract int ValueIndex
		{
			get;
			set;
		}
	}

	public class EnumTitlePair<TEnum> : EnumTitlePair where TEnum : struct
    {
		//TODO: tirar istro daquew
		public string TextNamespace { get; private set; }
		
		public event Action<EnumTitlePair<TEnum>, object> ValueChanged;
    
		public static IEnumerable<EnumTitlePair<TEnum>> GetAll(string textNamespace)
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(p => new EnumTitlePair<TEnum>(p, textNamespace));
        }

		public EnumTitlePair(TEnum value, string textNamespace)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new NotSupportedException(string.Format("'{0}' must be a Enum", typeof(TEnum).Name));
            }
            
			TextNamespace = textNamespace;
            Value = value;
        }
        
        public EnumTitlePair(string textNamespace)
		{
            if (!typeof(TEnum).IsEnum)
            {
                throw new NotSupportedException(string.Format("'{0}' must be a Enum", typeof(TEnum).Name));
            }
            
			TextNamespace = textNamespace;
			ValueIndex = -1;
		}

        public override bool Equals(object obj)
        {
            EnumTitlePair<TEnum> pair = obj as EnumTitlePair<TEnum>;
            if (pair == null)
            {
                return false;
            }
            return Value.Equals(pair.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        
        private object mValue;
        public override object Value
		{
			get
			{
				return mValue;
			}
			protected set
			{
				mValue = value;
			
				if(value == null)
				{
					mValueIndex = -1;
				}
				else
				{
					Title = Mvx.Resolve<IMvxTextProvider>().GetText(TextNamespace, typeof(TEnum).Name, value.ToString());
					Array options = Enum.GetValues(typeof(TEnum));
					mValueIndex = Array.IndexOf(options, value, 0, options.Length);
				}
				
				if(ValueChanged != null)
				{
					ValueChanged(this, value);
				}
			}
		}
        
		private int mValueIndex;
        public override int ValueIndex
		{
			get
			{
				return mValueIndex;
			}
			set
			{
				mValueIndex = value;
				
				if(value >= 0)
				{
					Value = Enum.GetValues(typeof(TEnum)).GetValue(value);
				}
				else
				{
					mValue = null;
					Title = null;
				
					if(ValueChanged != null)
					{
						ValueChanged(this, null);
					}
				}
			}
		}
    }
}
