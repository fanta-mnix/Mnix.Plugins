using System;
using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore;

namespace Mnix.Plugins.Contacts.Touch
{
	public class Plugin : IMvxPlugin
	{
		public void Load()
		{
			Mvx.ConstructAndRegisterSingleton<IMvxContactsManager, MvxContactsManager>();
		}
	}
}

