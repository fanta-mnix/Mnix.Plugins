using System;
using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore;

namespace Mnix.Plugins.Dialog.Touch
{
	public class Plugin : IMvxPlugin
	{
		public void Load()
		{
			Mvx.ConstructAndRegisterSingleton<IMvxDialogTask, MvxDialogTask>();
		}
	}
}

