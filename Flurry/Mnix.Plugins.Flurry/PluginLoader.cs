using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using System;

namespace Mnix.Plugins.Flurry
{
	public class PluginLoader : IMvxPluginLoader
	{
		public static readonly PluginLoader Instance = new PluginLoader();

		public void EnsureLoaded()
		{
			IMvxPluginManager manager = Mvx.Resolve<IMvxPluginManager>();
			manager.EnsurePlatformAdaptionLoaded<PluginLoader>();
		}
	}
}

