using System;
using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore;
using Mnix.Utils.Analytics;

namespace Mnix.Plugins.Flurry.Touch
{
	public class Plugin : IMvxPlugin
	{
		public void Load()
		{
			Mvx.RegisterSingleton<IFlurryAnalytics>(new FlurryTouchAnalytics());
		}
	}
}

