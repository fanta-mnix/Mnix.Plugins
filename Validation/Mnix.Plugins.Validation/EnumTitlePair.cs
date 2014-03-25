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

		public object Value
		{
			get;
			protected set;
		}
	}

	public class EnumTitlePair<TEnum> : EnumTitlePair where TEnum : struct
    {
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
            Title = Mvx.Resolve<IMvxTextProvider>().GetText(textNamespace, typeof(TEnum).Name, value.ToString());
            Value = value;
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
    }
}
