using System;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Mnix.Plugins.Validation.Touch.Views
{
	public class WarningEntryElement : EntryElement
	{
		public bool IsPassword  {
			get;
			private set;
		}

		public WarningTextField TextField
		{
			get;
			private set;
		}
		
		public string ErrorMessage
		{
			get
			{
				if(TextField != null)
				{
					return TextField.ErrorMessage;
				}
				else
				{
					return string.Empty;
				}
			}
			set
			{
				if(TextField != null)
				{
					TextField.ErrorMessage = value;
				}
			}
		}
		
		public event EventHandler Ended;

		public WarningEntryElement()
			: base("")
		{
			Setup();
		}

		public WarningEntryElement(string caption)
			: base(caption)
		{
			Setup();
		}

		public WarningEntryElement(string caption, string placeholder)
			: base(caption, placeholder)
		{
			Setup();
		}

		public WarningEntryElement(string caption, string placeholder, string value)
			: base(caption, placeholder, value)
		{
			Setup();
		}

		public WarningEntryElement(string caption, string placeholder, string value, bool isPassword)
			: base(caption, placeholder, value, isPassword)
		{
			IsPassword = isPassword;
			Setup();
		}

		private void Setup()
		{
			TextField = new WarningTextField()
			{
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleLeftMargin,
				Placeholder = Placeholder ?? "",
				SecureTextEntry = IsPassword,
				Text = Value ?? "",
				Tag = 1
			};
			TextField.Ended += delegate(object sender, EventArgs e)
			{
				if(Ended != null)
				{
					Ended(this, e);
				}
			};
		}

		protected override MonoTouch.UIKit.UITextField CreateTextField(System.Drawing.RectangleF frame)
		{
			TextField.Frame = frame;
			return TextField;
		}

		protected override UITableViewCell GetCellImpl(UITableView tv)
		{
			var cell = base.GetCellImpl(tv);
			cell.ContentView.ClipsToBounds = false;
			cell.ContentView.Superview.ClipsToBounds = false;
			cell.ClipsToBounds = false;

			return cell;
		}
	}
}

