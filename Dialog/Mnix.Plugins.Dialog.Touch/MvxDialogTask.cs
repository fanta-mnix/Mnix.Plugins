using System;
using MonoTouch.UIKit;

namespace Mnix.Plugins.Dialog.Touch
{
	public class MvxDialogTask : IMvxDialogTask
	{
		#region IMvxDialogTask implementation

		public void Show(string title, string message, string positiveText, string negativeText, Action yesCallback, Action noCallback)
		{
			UIAlertView dialog = new UIAlertView(
				                     title,
				                     message,
				                     null,
				                     negativeText,
				                     positiveText
			                     );
			dialog.Clicked += delegate(object sender, UIButtonEventArgs e)
			{
				if(e.ButtonIndex == dialog.CancelButtonIndex)
				{
					if(noCallback != null)
					{
						noCallback();
					}
				}
				else if(yesCallback != null)
				{
					yesCallback();
				}
			};

			dialog.Show();
		}
		
		public void Show(string message)
		{
			string title;
			
			if(message.Length <= 20)
			{
				title = message;
				message = string.Empty;
			}
			else
			{
				title = string.Empty;
			}
		
			new UIAlertView(
				title,
				message,
				null,
				"Ok"
			).Show();
		}

		#endregion
	}
}

